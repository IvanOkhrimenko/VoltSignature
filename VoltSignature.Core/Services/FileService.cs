using System;
using System.Threading.Tasks;
using VoltSignature.Common.Exceptions;
using VoltSignature.DbCore.Entity; 
using VoltSignature.DbCore.Repository;
using VoltSignature.Files.Interface;
using VoltSignature.Files.Model;
using VoltSignature.Interface;
using VoltSignature.Model.Account; 
using VoltSignature.Model.User;

namespace VoltSignature.Core.Services
{
    public class FileService : IFileService
    {
        private CertificateManager _certificator; 
        private IStorage _storage; 

        public FileService(IStorage storage)
        { 
            _storage = storage; 
            _certificator = new CertificateManager();
        }

        public async Task<FileModel> GeneratePrivateKey(CurrentUser currentUser)
        {
            var userRepository = _storage.GetRepository<User>();
            var certificateFileRepository = _storage.GetFileRepository(FileTypeEnum.Certificate);
            var keypair = new UserKeyPair(_certificator.GeneratePair());
            var certificate = _certificator.GenerateCertificate(currentUser.FullName, RegisterTokenOptions.ISSUER, keypair.PrivatKey, keypair.PublicKey);
            var user = await userRepository.Get(x => x.Id == currentUser.Id);
            string name = "certificate_" + user.FullName.Replace(" ", "_");
            user.CertificateId = await certificateFileRepository.Save(_certificator.ConvertToByte(certificate), name);
            await userRepository.Update(user);
            FileModel file = new FileModel(Guid.NewGuid() + ".pkf", keypair.PrivatKeyByte);
            return file;
        }

        public async Task<FileModel> GetFileForSignature(string fileId, string signatureId, CurrentUser currentUser)
        {
            var fileRepository = _storage.GetFileRepository(FileTypeEnum.SignatureFile);
            var signatureRepository = _storage.GetRepository<Signature>();
            var signature = await signatureRepository.Get(x => x.Id == signatureId);
            if (signature == null)
                throw new SignatureException("Not found signature with id " + signatureId, System.Net.HttpStatusCode.NotFound);
            if (!signature.UserSignatures.Exists(x => x.UserId == currentUser.Id) && signature.AuthorId != currentUser.Id)
                throw new SignatureException("No access", System.Net.HttpStatusCode.Unauthorized);
            if (signature.FileId != fileId)
                throw new SignatureException("Invalid file id", System.Net.HttpStatusCode.BadRequest);
            var file = await fileRepository.Get(fileId); 
            return file ?? throw new SignatureException("File not found", System.Net.HttpStatusCode.NotFound);
        }

        public async Task<FileModel> GetImage(string id)
        {
            var imageRepository = _storage.GetFileRepository(FileTypeEnum.Image);
            var file = await imageRepository.Get(id);
            if (file == null)
                throw new SignatureException($"Not found image with id {id}", System.Net.HttpStatusCode.NotFound); 
            return file;  
        }


 
 
    }
}