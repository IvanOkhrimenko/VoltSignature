using System;
using System.Reflection;
using VoltSignature.DbCore.Context;
using VoltSignature.DbCore.Entity.Base;
using VoltSignature.DbCore.Repository;
using VoltSignature.Files;
using VoltSignature.Files.Interface;
using VoltSignature.Files.Model;
using VoltSignature.Files.Repository;
using VoltSignature.Interface;

namespace VoltSignature.Core.Storage
{
    public class Storage : IStorage
    {
        private readonly MongoContext _context;
        private readonly IFileStorage _fileStorage;

        public Storage(MongoContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public IRepository<T> GetRepository<T>() where T : IEntity<string>
        {
            string collectionName = typeof(T).GetTypeInfo().GetCustomAttribute<CollectionNameAttribute>()?.Name;
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("Not set collection name in type " + typeof(T).GetType().Name);
            return new MongoRepository<T>(_context.GetCollection<T>(collectionName));
        }


        public IFileRepository GetFileRepository(FileTypeEnum fileType) 
        {
            return new FileRepository(_fileStorage, fileType);
        }
    }
}