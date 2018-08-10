using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks; 

namespace VoltSignature.MongoDb.Context
{
    public class SignatureImageContext : IStoreImage
    {
        IMongoDatabase _database; 
        IGridFSBucket _gridFS;

        public SignatureImageContext(string connectionString)
        {
            var connection = new MongoUrlBuilder(connectionString); 
            MongoClient client = new MongoClient(connectionString); 
            _database = client.GetDatabase(connection.DatabaseName); 
            _gridFS = new GridFSBucket(_database);
        }

        public async Task<byte[]> GetImage(string id)
        {
            return await _gridFS.DownloadAsBytesAsync(new ObjectId(id));
        }

        public async Task RemoveImage(string id)
        {
            await _gridFS.DeleteAsync(new ObjectId(id));
        }

        public async Task<string> SaveImage(Stream imageStream, string imageName)
        {
            ObjectId imageId = await _gridFS.UploadFromStreamAsync(imageName, imageStream);
            return imageId.ToString();
        }
    }
}
