using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VoltSignature.Model.Company;

namespace VoltSignature.Interface
{
    public interface ICompanyService
    {
        Task<CompanyModel> GetCompany(int Id);
    }
}
