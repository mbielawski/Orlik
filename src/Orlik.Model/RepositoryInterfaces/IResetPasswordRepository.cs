using System;
using Orlik.Model.Domain;

namespace Orlik.Model.RepositoryInterfaces
{
    public interface IResetPasswordRepository : IRepository
    {
        void Add(ResetPassword resetPassword);
        ResetPassword GetByToken(Guid token);
        ResetPassword GetByUserId(int userId);
        void Remove(int userId);
    }
}
