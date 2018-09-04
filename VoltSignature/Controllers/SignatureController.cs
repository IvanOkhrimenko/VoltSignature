using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VoltSignature.Model.Signature;

namespace VoltSignature.UI.Controllers
{ 
    [ApiController]
    public class SignatureController : BaseController
    {
        public SignatureController()
        {

        }

        [HttpPost("api/signature")]
        public IActionResult CreateSignatureRequest([FromBody]SignatureRequest model)
        {
            throw new NotImplementedException();
        }
    }
}