using System;
using Orlik.Model.Domain;

namespace Orlik.Infrastructure.Services
{
    public interface IResetPasswordService : IService
    {
        void Create(User user);
        ResetPassword GetByToken(Guid token);
        void Delete(int userId);
    }
}
