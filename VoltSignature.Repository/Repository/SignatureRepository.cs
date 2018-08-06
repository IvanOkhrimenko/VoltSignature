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
    public class SignatureRepository : BaseRepository//, IRepository<Signature>
    {
        public SignatureRepository(DbSignatureContext context) : base(context)
        {
        }

        //public async Task<Signature> Get(Expression<Func<Signature, bool>> expression, Func<IQueryable<Signature>, IIncludableQueryable<Signature, object>> include = null, bool asNoTracking = false)
        //{
        //    return await GetBase(_context.Signatures.AsQueryable(), expression, include, asNoTracking);
        //}

        //public async Task<List<Signature>> GetList(Expression<Func<Signature, bool>> expression, Func<IQueryable<Signature>, IIncludableQueryable<Signature, object>> include = null, bool asNoTracking = false)
        //{
        //    return await GetListBase(_context.Signatures.AsQueryable(), expression, include, asNoTracking);
        //}

        //public async Task Update(Signature signature)
        //{
        //    await UpdateBase(_context.Signatures, signature);
        //}

        //public async Task Remove(Signature signature)
        //{
        //    await RemoveBase(_context.Signatures, signature);
        //}

        //public async Task Create(Signature signature)
        //{
        //    await CreateBase(_context.Signatures, signature);
        //}
    }
}