using System;
using Orlik.Common.Extensions;
using Orlik.Common.Factories;
using Orlik.Common.Utilities;
using Orlik.Model.Domain;
using Orlik.Model.RepositoryInterfaces;

namespace Orlik.Infrastructure.Services
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly IResetPasswordRepository _resetPasswordRepository;
        private readonly IEmailSender _emailSender;

        public ResetPasswordService(IResetPasswordRepository resetPasswordRepository, IEmailSender emailSender)
        {
            _resetPasswordRepository = resetPasswordRepository;
            _emailSender = emailSender;
        }

        public void Create(User user)
        {
            var token = Guid.NewGuid();
            var resetPassword = new ResetPassword(user.UserId, token);

            if (_resetPasswordRepository.GetByUserId(user.UserId) != null)
                _resetPasswordRepository.Remove(user.UserId);

            _resetPasswordRepository.Add(resetPassword);

            var message = ResetPasswordMessageFactory.CreateResetPasswordMessage(user.Email, token);
            _emailSender.SendEmail(message);
        }

        public ResetPassword GetByToken(Guid token)
        {
            var resetPassword = _resetPasswordRepository.GetByToken(token);

            return resetPassword.ExpirationTime.IsGreaterThanToday() ? resetPassword : null;
        }

        public void Delete(int userId)
            => _resetPasswordRepository.Remove(userId);
    }
}
