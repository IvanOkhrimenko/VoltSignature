using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VoltSignature.Model.User;

namespace VoltSignature.Interface
{
    public interface IUserService
    {
        Task<UserModel> GetUser(int Id);
    }
}
