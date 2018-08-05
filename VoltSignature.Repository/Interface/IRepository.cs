using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using VoltSignature.PostgreSQL.Entity;

namespace VoltSignature.Repository.Interface
{
    public interface IRepository<T>
    {
        Task Create(T entity);
        Task<T> Get(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool asNoTracking = false);
        Task<List<T>> GetList(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool asNoTracking = false);
        Task Remove(T entity);
        Task Update(T entity);
    }
}