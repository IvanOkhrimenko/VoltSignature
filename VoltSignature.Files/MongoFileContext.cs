using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using System.IO;
using System.Threading.Tasks;  
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

        public async Task<FileModel> Get(string id)
        {
            try
            {
                using (var stream = _gridFS.OpenDownloadStream(new ObjectId(id)))
                {
                    FileModel model = new FileModel(stream.FileInfo.Filename);
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

        public async Task Remove(string id)
        {
            await _gridFS.DeleteAsync(new ObjectId(id));
        }

        public async Task<string> Save(Stream imageStream, string imageName)
        {
            ObjectId imageId = await _gridFS.UploadFromStreamAsync(imageName, imageStream);
            return imageId.ToString();
        }

        public async Task<string> Save(byte[] image, string imageName)
        {
            using(MemoryStream ms = new MemoryStream(image))
            { 
                ObjectId imageId = await _gridFS.UploadFromStreamAsync(imageName, ms);
                return imageId.ToString();
            }

        }
    }
}