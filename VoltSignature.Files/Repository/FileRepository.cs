using System.IO;
using System.Threading.Tasks;
using VoltSignature.DbCore.Context;
using VoltSignature.Files.Interface;
using VoltSignature.Files.Model;

namespace VoltSignature.Files.Repository
{
    public class FileRepository : IFileRepository
    {
        private IFileStorage _fileContext;
        private readonly FileTypeEnum _fileType;

        public FileRepository(IFileStorage fileContext, FileTypeEnum fileType)
        {
            _fileContext = fileContext;
            _fileType = fileType;
        }

        public async Task<FileModel> Get(string id)
        {
            return await _fileContext.GetFile(id, _fileType);
        }

        public async Task Remove(string id)
        {
            await _fileContext.RemoveFile(id, _fileType);
        }

        public async Task<string> Save(MemoryStream ms, string fileName)
        {
            return await _fileContext.SaveFile(ms, fileName, _fileType);
        }

        public async Task<string> Save(byte[] file, string fileName)
        {
            using (MemoryStream ms = new MemoryStream(file))
            {
                return await _fileContext.SaveFile(ms, fileName, _fileType);
            }
        }
    }


}