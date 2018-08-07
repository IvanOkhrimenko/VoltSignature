using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VoltSignature.PostgreSQL.Context;
using VoltSignature.PostgreSQL.Entity;
using VoltSignature.Repository.Interface;

namespace VoltSignature.Repository.Repository
{
    public class UserRepository : BaseRepository, IRepository<User>
    {
        public UserRepository(DbSignatureContext context) : base(context)
        {
        }

        public async Task<User> Get(Expression<Func<User, bool>> expression, Func<IQueryable<User>, IIncludableQueryable<User, object>> include = null, bool asNoTracking = false)
        {
            return await GetBase(_context.Users.AsQueryable(), expression, include, asNoTracking);
        }

        public async Task<List<User>> GetList(Expression<Func<User, bool>> expression, Func<IQueryable<User>, IIncludableQueryable<User, object>> include = null, bool asNoTracking = false)
        {
            return await GetListBase(_context.Users.AsQueryable(), expression, include, asNoTracking);
        }

        public async Task Update(User user)
        {
            await UpdateBase(_context.Users, user);
        }

        public async Task Remove(User user)
        {
            await RemoveBase(_context.Users, user);
        }

        public async Task Create(User user)
        {
            await CreateBase(_context.Users, user);
        }

        public override Type GetEntityType() => typeof(User);
    }
}