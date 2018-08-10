using System.IO;
using System.Threading.Tasks;

namespace VoltSignature.MongoDb.Context
{
    public interface IStoreImage
    {
        Task<byte[]> GetImage(string id);
        Task RemoveImage(string id);
        Task<string> SaveImage(Stream imageStream, string imageName);
    }
}