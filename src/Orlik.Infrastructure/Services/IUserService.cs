using System;
using Orlik.Infrastructure.RequestModels;
using Orlik.Model.Domain;

namespace Orlik.Infrastructure.Services
{
    public interface IUserService : IService
    {
        void Register(UserRegisterRequestModel userRegisterRequestModel);
        bool SignIn(UserSignInRequestModel userSignInRequestModel);
        User GetUserByUsername(string username);
        User GetUserByEmail(string email);
        int GetIdByEmail(string email);
        void Activate(Guid activationId);
        void ChangePassword(string email, string password, string oldPassword = null);
    }
}
