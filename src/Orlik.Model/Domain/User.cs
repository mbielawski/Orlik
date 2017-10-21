using Orlik.Common.Extensions;
using Orlik.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Orlik.Model.Domain
{
    public class User
    {
        private static readonly Regex Regex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");

        public int UserId { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string FirstName { get; protected set; }
        public string SecondName { get; protected set; }
        public string Email { get; protected set; }
        public Guid ActivationId { get; protected set; }
        public bool IsActive { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public virtual IList<UserReservation> UserReservations { get; set; }

        protected User()
        {
        }

        public User(string username, string firstName, string secondName, string email, Guid activationId)
        {
            SetUsername(username);
            SetFirstName(firstName);
            SetSecondName(secondName);
            SetEmail(email);
            SetActivationId(activationId);
            IsActive = false;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetUsername(string username)
        {
            if (username.Empty())
                throw new ArgumentException("Username can not be empty.");

            if (Username.EqualsCaseInvariant(username))
                return;

            if (username.Length < 3)
                throw new ArgumentException("Username is too short.");

            if (username.Length > 20)
                throw new ArgumentException("Username is too long.");

            if (Regex.IsMatch(username) == false)
                throw new ArgumentException("Username doesn't meet the required criteria.");

            Username = username.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (password.Empty())
                throw new ArgumentException("Password can not be empty.");

            if (password.Length < 6)
                throw new ArgumentException("Password is too short.");

            if (password.Length > 50)
                throw new ArgumentException("Password is too long.");

            var salt = encrypter.GetSalt(password);
            var hash = encrypter.GetHash(password, salt);

            Password = hash;
            Salt = salt;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool ValidatePassword(string password, IEncrypter encrypter)
        {
            var hashedPassword = encrypter.GetHash(password, Salt);

            return Password.Equals(hashedPassword);
        }

        public void SetFirstName(string firstName)
        {
            if (firstName.Empty())
                throw new ArgumentException("First name can not be empty.");

            if (FirstName.EqualsCaseInvariant(firstName))
                return;

            if (firstName.Length < 3)
                throw new ArgumentException("First name is too short.");

            if (firstName.Length > 20)
                throw new ArgumentException("First name is too long.");

            if (Regex.IsMatch(firstName) == false)
                throw new ArgumentException("First name doesn't meet the required criteria.");

            FirstName = firstName.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetSecondName(string secondName)
        {
            if (secondName.Empty())
                throw new ArgumentException("Second name can not be empty.");

            if (SecondName.EqualsCaseInvariant(secondName))
                return;

            if (secondName.Length < 3)
                throw new ArgumentException("Second name is too short.");

            if (secondName.Length > 20)
                throw new ArgumentException("Second name is too long.");

            if (Regex.IsMatch(secondName) == false)
                throw new ArgumentException("Second name doesn't meet the required criteria.");

            SecondName = secondName.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if (email.Empty())
                throw new ArgumentException("Email can not be empty.");

            if (!email.IsEmail())
                throw new ArgumentException("Invalid email.");

            if (Email.EqualsCaseInvariant(email))
                return;

            Email = email.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetActivationId(Guid activationId)
            => ActivationId = activationId;

        public void Activate()
            => IsActive = true;

        public void UpdateTimeNow()
            => UpdatedAt = DateTime.UtcNow;
    }
}
