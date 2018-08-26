using MongoDB.Driver;
using System;
using System.Reflection;
using System.Threading.Tasks;
using VoltSignature.DbCore.Entity.Base;
using VoltSignature.DbCore.Repository;

namespace VoltSignature.DbCore.Context
{
    public class MongoContext 
    {
        protected readonly IMongoDatabase _database;

        private MongoClient _client;

        public MongoContext(string connectionString)
        {
            var connection = new MongoUrlBuilder(connectionString);
            _client = new MongoClient(connectionString); 
             _database = _client.GetDatabase(connection.DatabaseName);
        }
    
        public Task CreateCollection(string name) => _database.CreateCollectionAsync(name);

        public IMongoCollection<T> GetCollection<T>(string name) => _database.GetCollection<T>(name);
    }
}