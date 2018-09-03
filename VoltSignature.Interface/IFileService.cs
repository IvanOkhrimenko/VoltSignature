using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VoltSignature.Model.Files;
using VoltSignature.Model.User;

namespace VoltSignature.Interface
{
    public interface IFileService
    {
        Task<FileModel> GeneratePrivateKey(CurrentUser currentUser);
    }
}
