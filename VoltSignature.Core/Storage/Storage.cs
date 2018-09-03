using System;
using System.Reflection;
using VoltSignature.DbCore.Context;
using VoltSignature.DbCore.Entity.Base;
using VoltSignature.DbCore.Repository;
using VoltSignature.Interface;

namespace VoltSignature.Core.Storage
{
    public class Storage : IStorage
    {
        private MongoContext _context;

        public Storage(MongoContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : IEntity<string>
        {
            string collectionName = typeof(T).GetTypeInfo().GetCustomAttribute<CollectionNameAttribute>()?.Name;
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("Not set collection name in type " + typeof(T).GetType().Name);
            return new MongoRepository<T>(_context.GetCollection<T>(collectionName));
        }
    }
}