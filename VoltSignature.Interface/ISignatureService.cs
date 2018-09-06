using System.Collections.Generic;
using System.Threading.Tasks;
using VoltSignature.Model.Signature;
using VoltSignature.Model.User;

namespace VoltSignature.Interface
{
    public interface ISignatureService
    {
        Task CreateSignatureRequest(SignatureRequest model, string userId);
        Task<List<SignatureModel>> GetForSignature(CurrentUser user);
    }
}