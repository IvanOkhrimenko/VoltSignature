using System.Threading.Tasks;
using VoltSignature.Model.Company;

namespace VoltSignature.Interface
{
    public interface ICompanyService
    {
        Task<CompanyModel> GetCompany(string Id);
    }
}