using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoltSignature.Interface;

namespace VoltSignature.UI.Controllers
{ 
    public class FilesController : BaseController
    {
        IFileService _fileService;
         

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet("/file/generateCertificate")] 
        public async Task<FileResult> GenerateCertificate()
        {
            var file = await _fileService.GeneratePrivateKey(CurrentUser);
            return File(file.Data, "application/octet-stream", file.Name);
        }
    }
}