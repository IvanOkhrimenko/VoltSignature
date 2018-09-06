using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using VoltSignature.Files.Model;

namespace VoltSignature.Files.Repository
{
    public interface IFileRepository
    {
        Task<FileModel> Get(string id);
        Task Remove(string id);
        Task<string> Save(MemoryStream ms, string fileName);
        Task<string> Save(byte[] file, string fileName);
        Task<string> GetName(string id);
    }
}
