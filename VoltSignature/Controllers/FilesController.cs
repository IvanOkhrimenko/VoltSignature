using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VoltSignature.Interface;

namespace VoltSignature.UI.Controllers
{
    public class FilesController : BaseController
    {
        private IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet("/file/generateCertificate")]
        public async Task<FileResult> GeneratePrivateKey()
        {
            var file = await _fileService.GeneratePrivateKey(CurrentUser);
            return File(file.Data, "application/octet-stream", file.Name);
        }

        [HttpGet("/file/signature/{fileId}/{signatureId}")]
        public async Task<FileResult> GetFileForSignature(string fileId, string signatureId)
        {
            var file = await _fileService.GetFileForSignature(fileId, signatureId, CurrentUser);
            return File(file.Data, "application/octet-stream", file.Name);
        }

        [HttpGet("/file/image/{id}")]
        public async Task<FileResult> GetImage(string id)
        {
            var image = await _fileService.GetImage(id);
            return File(image.Data, "image/*", image.Name);
        }
    }
}