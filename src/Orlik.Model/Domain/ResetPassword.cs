using System;

namespace Orlik.Model.Domain
{
    public class ResetPassword
    {
        public int ResetPasswordId { get; protected set; }
        public int UserId { get; protected set; }
        public Guid Token { get; protected set; }
        public DateTime ExpirationTime { get; protected set; }

        protected ResetPassword()
        {
        }

        public ResetPassword(int userId, Guid token)
        {
            SetUserId(userId);
            SetToken(token);
            ExpirationTime = DateTime.UtcNow.AddHours(1);
        }

        public void SetUserId(int userId)
        {
            UserId = userId;
        }

        public void SetToken(Guid token)
        {
            Token = token;
        }
    }
}
