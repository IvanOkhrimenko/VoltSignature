using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace VoltSignature.DbCore.Repository
{
    public interface IRepository<T>
    {
        Task<T> Get(Expression<Func<T, bool>> expression);

        Task Create(T entity);

        Task Update(T entity);

        Task Remove(string id);

        Task<List<T>> GetList(Expression<Func<T, bool>> expression);
    }
}