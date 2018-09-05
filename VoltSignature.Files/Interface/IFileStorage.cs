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
        Task RemoveFile(string id, FileTypeEnum fileType);
        Task<string> SaveFile(MemoryStream ms, string fileName, FileTypeEnum fileType);
        Task<FileModel> GetFile(string id, FileTypeEnum fileType); 
    }
}
