using System;
using System.Linq;
using Orlik.Infrastructure.EF;
using Orlik.Model.Domain;
using Orlik.Model.RepositoryInterfaces;

namespace Orlik.Infrastructure.Repositories
{
    public class ResetPasswordRepository : IResetPasswordRepository
    {
        private readonly OrlikContext _context;

        public ResetPasswordRepository(OrlikContext context)
        {
            _context = context;
        }

        public void Add(ResetPassword resetPassword)
        {
            _context.ResetPasswords.Add(resetPassword);
            _context.Save();
        }

        public ResetPassword GetByToken(Guid token) 
            => _context.ResetPasswords.SingleOrDefault(x => x.Token == token);

        public ResetPassword GetByUserId(int userId)
            => _context.ResetPasswords.SingleOrDefault(x => x.UserId == userId);

        public void Remove(int userId)
        {
            var resetPassword = GetByUserId(userId);
            _context.ResetPasswords.Remove(resetPassword);
            _context.Save();
        }
    }
}
