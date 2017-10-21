using System;
using Orlik.Model.Domain;

namespace Orlik.Model.RepositoryInterfaces
{
    public interface IUserRepository : IRepository
    {
        User GetById(int id);
        User GetByUsername(string username);
        User GetByEmail(string email);
        User GetByActivationId(Guid activationId);
        void Add(User user);
        void Update(User user);
        void Remove(int id);
    }
}
