using AutoMapper;
using ExpressionBuilder.Common;
using ExpressionBuilder.Generics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VoltSignature.DbCore.Entity;
using VoltSignature.DbCore.Repository;
using VoltSignature.Interface;
using VoltSignature.Model.Company;

namespace VoltSignature.Core.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly IStorage _storage;
        private readonly IRepository<Company> _companyRepository;

        public CompanyService(IMapper mapper, IStorage storage)
        {
            _mapper = mapper;
            _storage = storage;
            _companyRepository = _storage.GetRepository<Company>();
        }

        public async Task<CompanyModel> GetCompany(string Id)
        {
            var company = await _companyRepository.Get(x => x.Id == Id);
            if (company == null)
                throw new Exception("Not found company by id " + Id);
            var result = _mapper.Map<Company, CompanyModel>(company);
            return result;
        }

        public async Task<List<CompanyModel>> FindCompanys(string name)
        {
            var filter = new Filter<Company>();
            if (!string.IsNullOrEmpty(name))
                filter.By("Name", Operation.Contains, name, connector: FilterStatementConnector.Or); 
            List<Company> users = await _companyRepository.GetList(filter.Expression);
            return _mapper.Map<List<Company>, List<CompanyModel>>(users);
        }
    }
}