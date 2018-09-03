using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using System.IO;
using System.Threading.Tasks; 
using VoltSignature.DbCore.Interface;

namespace VoltSignature.DbCore.Context
{
    public class MongoFileContext : MongoContext, IFileStorage
    {
        private IGridFSBucket _gridFS;

        public MongoFileContext(string connectionString) : base(connectionString)
        {
            _gridFS = new GridFSBucket(_database);
        }

        public async Task<byte[]> Get(string id)
        {
            return await _gridFS.DownloadAsBytesAsync(new ObjectId(id));
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