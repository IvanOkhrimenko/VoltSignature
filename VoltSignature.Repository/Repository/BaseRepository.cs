using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VoltSignature.PostgreSQL.Context;

namespace VoltSignature.Repository.Repository
{
    public class BaseRepository
    {
        protected readonly DbSignatureContext _context;

        public BaseRepository(DbSignatureContext context)
        {
            _context = context;
        }

        protected IQueryable<T> BuildQuery<T>(IQueryable<T> query, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, bool asNoTracking) where T : class
        {
            if (include != null)
                query = include(query);
            if (asNoTracking)
                query = query.AsNoTracking();
            return query;
        }

        public async Task<T> GetBase<T>(IQueryable<T> source, Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool asNoTracking = false) where T : class
        { 
            return await BuildQuery(source, include, asNoTracking).FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> GetListBase<T>(IQueryable<T> source,Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool asNoTracking = false) where T : class
        { 
            return await BuildQuery(source, include, asNoTracking).Where(expression).ToListAsync();
        }

        public async Task UpdateBase<T>(DbSet<T> source,T entity) where T : class
        {
            source.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveBase<T>(DbSet<T> source, T entity) where T : class
        {
            source.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task CreateBase<T>(DbSet<T> source, T entity) where T : class
        {
            source.Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}