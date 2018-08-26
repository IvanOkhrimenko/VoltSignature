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

        string GenerateRegistrationToken(RegistrationParameters parameters);
    }
}