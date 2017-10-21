using System;
using Orlik.Model.Domain;
using Orlik.Infrastructure.EF;
using System.Linq;
using System.Data.Entity.Migrations;
using Orlik.Model.RepositoryInterfaces;

namespace Orlik.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly OrlikContext _context;

        public UserRepository(OrlikContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.Save();
        }

        public User GetById(int id)
            => _context.Users.SingleOrDefault(x => x.UserId == id);

        public User GetByUsername(string username)
            => _context.Users.SingleOrDefault(x => x.Username == username);

        public User GetByEmail(string email)
            => _context.Users.SingleOrDefault(x => x.Email == email);

        public User GetByActivationId(Guid activationId)
            => _context.Users.SingleOrDefault(x => x.ActivationId == activationId);

        public void Remove(int id)
        {
            var user = GetById(id);
            _context.Users.Remove(user);
            _context.Save();
        }

        public void Update(User user)
        {
            user.UpdateTimeNow();
            _context.Users.AddOrUpdate(user);
            _context.Save();
        }
    }
}
