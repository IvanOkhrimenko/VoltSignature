using System.Collections.Generic;
using System.Threading.Tasks;
using VoltSignature.Model.Account;
using VoltSignature.Model.User;

namespace VoltSignature.Interface
{
    public interface IUserService
    {
        Task<UserModel> GetUser(string Id);

        Task<UserModel> LoginUser(string login, string pwd);

        Task<UserModel> RegisterUser(RegisterModel model, RegistrationParameters parameters);

        Task<List<UserModel>> FindUsers(string first, string last, string email);

        string GenerateRegistrationToken(RegistrationParameters parameters);
    }
}