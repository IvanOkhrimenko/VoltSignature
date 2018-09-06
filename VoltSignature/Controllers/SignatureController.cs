using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VoltSignature.Common.Exceptions;
using VoltSignature.Interface;
using VoltSignature.Model.Signature;

namespace VoltSignature.UI.Controllers
{ 
    public class SignatureController : BaseController
    {
        private readonly ISignatureService _signatureService;

        public SignatureController(ISignatureService signatureService)
        {
            _signatureService = signatureService;
        }


        [HttpGet("api/signature")]
        public async Task<List<SignatureModel>> CreateSignatureRequest()
        { 
            var result = await _signatureService.GetForSignature(CurrentUser);
            return result;
        }

        [HttpPost("api/signature")]
        public async Task<IActionResult> CreateSignatureRequest([FromBody]SignatureRequest model)
        {
            if (!ModelState.IsValid)
                throw new SignatureException("Not valid data to signature", System.Net.HttpStatusCode.BadRequest);
            await _signatureService.CreateSignatureRequest(model, CurrentUser.Id);
            return Ok();
        }
    }
}