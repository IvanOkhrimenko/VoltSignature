using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VoltSignature.Interface;
using VoltSignature.Model.Company;
using VoltSignature.Model.User;

namespace VoltSignature.UI.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICompanyService _companyService;

        public SearchController(IUserService userService, ICompanyService companyService)
        {
            _userService = userService;
            _companyService = companyService;
        }

        [HttpGet("user")]
        public async Task<List<UserModel>> FindUser(string first, string last, string email)
        {
            var users = await _userService.FindUsers(first, last, email);
            return users;
        }

        [HttpGet("company")]
        public async Task<List<CompanyModel>> FindCompany(string name)
        {
            var companys = await _companyService.FindCompanys(name);
            return companys;
        }
    }
}