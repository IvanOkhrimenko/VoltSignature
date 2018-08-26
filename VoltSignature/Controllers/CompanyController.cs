using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VoltSignature.Interface;
using VoltSignature.Model.Company;

namespace VoltSignature.UI.Controllers
{
    [Route("api/company")]
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("{Id?}")]
        public async Task<CompanyModel> Get(string Id)
        {
            var company = await _companyService.GetCompany(Id ?? CurrentUser.CompanyId);
            return company;
        }
    }
}