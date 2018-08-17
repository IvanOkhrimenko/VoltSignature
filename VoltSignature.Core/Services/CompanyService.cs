using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VoltSignature.Interface;
using VoltSignature.Model.Company;
using VoltSignature.PostgreSQL.Entity;
using VoltSignature.Repository.Interface;

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
            _companyRepository = _storage.Get<Company>();
        }

        public async Task<CompanyModel> GetCompany(int Id)
        {
            var company = await  _companyRepository.Get(x => x.Id == Id, asNoTracking: true);
            if (company == null)
                throw new Exception("Not found company by id " + Id);
            var result = _mapper.Map<Company, CompanyModel>(company);
            return result;
        }
    }
}
