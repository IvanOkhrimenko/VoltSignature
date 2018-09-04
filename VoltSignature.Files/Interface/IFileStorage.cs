using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using VoltSignature.Files.Model;

namespace VoltSignature.Files.Interface
{
    public interface IFileStorage
    {
        Task<FileModel> Get(string id);
        Task Remove(string id);
        Task<string> Save(Stream imageStream, string imageName);
        Task<string> Save(byte[] image, string imageName);
    }
}
