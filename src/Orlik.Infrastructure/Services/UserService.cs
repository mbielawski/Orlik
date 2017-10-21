using Orlik.Common.Extensions;
using Orlik.Common.Utilities;
using Orlik.Model.Domain;
using System;
using Orlik.Common.Factories;
using Orlik.Infrastructure.RequestModels;
using Orlik.Model.RepositoryInterfaces;

namespace Orlik.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly IEmailSender _emailSender;

        public UserService(IUserRepository userRepository, IEncrypter encrypter, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _emailSender = emailSender;
        }

        public void Register(UserRegisterRequestModel userRegisterRequestModel)
        {
            var user = _userRepository.GetByUsername(userRegisterRequestModel.Username);
            if (user != null)
                throw new Exception($"User with username: '{ userRegisterRequestModel.Username }' already exists.");

            user = _userRepository.GetByEmail(userRegisterRequestModel.Email);
            if (user != null)
                throw new Exception($"User with email: '{ userRegisterRequestModel.Email }' already exists.");

            var activationId = Guid.NewGuid();
            user = new User(userRegisterRequestModel.Username,
                userRegisterRequestModel.FirstName,
                userRegisterRequestModel.SecondName,
                userRegisterRequestModel.Email,
                activationId);

            if (userRegisterRequestModel.Password.Empty())
                throw new Exception(nameof(userRegisterRequestModel.Password));

            user.SetPassword(userRegisterRequestModel.Password, _encrypter);
            var message = ActivateMessageFactory.CreateActivationMessage(userRegisterRequestModel.Email, activationId);
            _emailSender.SendEmail(message);
            _userRepository.Add(user);
        }

        public bool SignIn(UserSignInRequestModel userSignInRequestModel)
        {
            var user = _userRepository.GetByUsername(userSignInRequestModel.Username);
            if (user == null)
                throw new Exception($"User with username: '{ userSignInRequestModel.Username }' doesn't exists.");

            var hash = _encrypter.GetHash(userSignInRequestModel.Password, user.Salt);

            if (hash != user.Password)
                throw new Exception("Wrong password.");

            return user.IsActive;
        }

        public User GetUserByUsername(string username) =>
            _userRepository.GetByUsername(username);

        public User GetUserByEmail(string email)
            => _userRepository.GetByEmail(email);

        public int GetIdByEmail(string email)
            => GetUserByEmail(email).UserId;

        public void Activate(Guid activationId)
        {
            var user = _userRepository.GetByActivationId(activationId);
            if (user == null)
                throw new Exception("Invalid activationId.");

            user.Activate();

            _userRepository.Update(user);
        }

        public void ChangePassword(string email, string password, string oldPassword = null)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null)
                throw new Exception(nameof(user));

            if (password.Empty())
                throw new Exception(nameof(password));

            if (oldPassword == null)
            {
                user.SetPassword(password, _encrypter);
                _userRepository.Update(user);
            }
            else if (IsPasswordEqualToUserPassword(user, oldPassword))
            {
                user.SetPassword(password, _encrypter);
                _userRepository.Update(user);
            }
            else
            {
                throw new Exception(nameof(oldPassword));
            }
        }

        private bool IsPasswordEqualToUserPassword(User user, string password)
            => _encrypter.GetHash(password, user.Salt) == user.Password;
    }
}
