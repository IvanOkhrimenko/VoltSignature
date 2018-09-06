using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using VoltSignature.DbCore.Context;
using VoltSignature.DbCore.Entity.Base;

namespace VoltSignature.DbCore.Repository
{
    public class MongoRepository<T> : IRepository<T> where T : IEntity<string>
    {
        public MongoRepository(IMongoCollection<T> collection)
        {
            Collection = collection;
        }

        protected IMongoCollection<T> Collection { get; }

        public async Task<T> Get(Expression<Func<T, bool>> expression)
        {  
            return await Collection.Find(expression).FirstOrDefaultAsync();
        }

        public async Task Create(T entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        public async Task Update(T entity)
        {
            await Collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public async Task Remove(string id)
        {
            await Collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<long> Count(Expression<Func<T, bool>> expression)
        {
            return await Collection.CountDocumentsAsync(expression);
        }

        public async Task<List<T>> GetList(Expression<Func<T, bool>> expression)
        {
            return await Collection.Find(expression).ToListAsync();
        }
    }
}