using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VoltSignature.Model.Account;
using VoltSignature.Model.User;
using VoltSignature.PostgreSQL.Entity;

namespace VoltSignature.Interface
{
    public interface IUserService
    {
        Task<UserModel> GetUser(int Id);
        Task<User> RegisterUser(RegisterModel model, RegistrationParameters parameters);
        string GenerateRegistrationToken(RegistrationParameters parameters);
    }
}
