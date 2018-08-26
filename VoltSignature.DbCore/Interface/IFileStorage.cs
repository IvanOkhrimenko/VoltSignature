using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace VoltSignature.DbCore.Interface
{
    public interface IFileStorage
    {
        Task<byte[]> Get(string id);
        Task Remove(string id);
        Task<string> Save(Stream imageStream, string imageName);
    }
}
