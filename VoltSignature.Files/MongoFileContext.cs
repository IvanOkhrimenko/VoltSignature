using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.IO;
using System.Threading.Tasks;
using VoltSignature.Files;
using VoltSignature.Files.Interface;
using VoltSignature.Files.Model;

namespace VoltSignature.DbCore.Context
{
    public class MongoFileContext : MongoContext, IFileStorage
    {
        private IGridFSBucket _gridFS;

        public MongoFileContext(string connectionString) : base(connectionString)
        {
            _gridFS = new GridFSBucket(_database);
        }

        public async Task RemoveFile(string id, FileTypeEnum fileType)
        {
            var file = await GetFile(id, fileType);
            if (file != null)
                await _gridFS.DeleteAsync(new ObjectId(id));
        }

        public async Task<string> SaveFile(MemoryStream ms, string fileName, FileTypeEnum fileType)
        {
            GridFSUploadOptions options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument()
                {
                    { "type",fileType }
                }
            };
            ObjectId imageId = await _gridFS.UploadFromStreamAsync(fileName, ms, options);
            return imageId.ToString();
        }

        public async Task<string> GetFileName(string id, FileTypeEnum fileType)
        { 
            var builder = Builders<GridFSFileInfo>.Filter;
            var filter = builder.Eq("_id", new ObjectId(id)) & builder.Eq("Metadata.type", (int)fileType);
            var cursor = await _gridFS.FindAsync(filter);
            var result = cursor.FirstOrDefault();
            return result?.Filename;
        }

        public async Task<FileModel> GetFile(string id, FileTypeEnum fileType)
        {
            try
            {
                using (var stream = _gridFS.OpenDownloadStream(new ObjectId(id)))
                {
                    FileModel model = new FileModel(stream.FileInfo.Filename);
                    if (fileType != (FileTypeEnum)stream.FileInfo.Metadata["type"].AsInt32)
                        return null;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        await stream.CopyToAsync(ms);
                        model.Data = ms.ToArray();
                    }
                    stream.Close();
                    return model;
                }
            }
            catch
            {
                return null;
            }

        }


    }
}