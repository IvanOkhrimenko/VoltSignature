using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VoltSignature.PostgreSQL.Context;
using VoltSignature.PostgreSQL.Entity;
using VoltSignature.Repository.Interface;

namespace VoltSignature.Repository.Repository
{
    public class UserSignatureRepository : BaseRepository, IRepository<UserSignature>
    {
        public UserSignatureRepository(DbSignatureContext context) : base(context)
        {
        }

        public async Task<UserSignature> Get(Expression<Func<UserSignature, bool>> expression, Func<IQueryable<UserSignature>, IIncludableQueryable<UserSignature, object>> include = null, bool asNoTracking = false)
        {
            return await GetBase(_context.UserSignatures.AsQueryable(), expression, include, asNoTracking);
        }

        public async Task<List<UserSignature>> GetList(Expression<Func<UserSignature, bool>> expression, Func<IQueryable<UserSignature>, IIncludableQueryable<UserSignature, object>> include = null, bool asNoTracking = false)
        {
            return await GetListBase(_context.UserSignatures.AsQueryable(), expression, include, asNoTracking);
        }

        public async Task Update(UserSignature userSignature)
        {
            await UpdateBase(_context.UserSignatures, userSignature);
        }

        public async Task Remove(UserSignature userSignature)
        {
            await RemoveBase(_context.UserSignatures, userSignature);
        }

        public async Task Create(UserSignature userSignature)
        {
            await CreateBase(_context.UserSignatures, userSignature);
        }

        public override Type GetEntityType() => typeof(UserSignature);
    }
}
