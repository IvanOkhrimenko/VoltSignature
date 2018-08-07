using Microsoft.EntityFrameworkCore;
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
    public class CompanyRepository : BaseRepository, IRepository<Company>
    {
        public CompanyRepository(DbSignatureContext context) : base(context)
        {
        }

        public async Task<Company> Get(Expression<Func<Company,bool>> expression, Func<IQueryable<Company>, IIncludableQueryable<Company, object>> include=null, bool asNoTracking=false)
        {
            return await GetBase(_context.Companies.AsQueryable(), expression, include, asNoTracking);
        }

        public async Task<List<Company>> GetList(Expression<Func<Company, bool>> expression, Func<IQueryable<Company>, IIncludableQueryable<Company, object>> include = null, bool asNoTracking = false)
        {
            return await GetListBase(_context.Companies.AsQueryable(), expression, include, asNoTracking); 
        }

        public async Task Update(Company company)
        {
            await UpdateBase(_context.Companies, company); 
        }

        public async Task Remove(Company company)
        {
            await RemoveBase(_context.Companies, company);
        }

        public async Task Create(Company company)
        {
            await CreateBase(_context.Companies, company);
        }

        public override Type GetEntityType() => typeof(Company); 
    }
}
