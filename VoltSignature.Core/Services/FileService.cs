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
        private IFileStorage _fileContext;
        private IStorage _storage; 

        public FileService(IFileStorage fileContext, IStorage storage)
        {
            _fileContext = fileContext;
            _storage = storage; 
            _certificator = new CertificateManager();
        }

        public async Task<FileModel> GeneratePrivateKey(CurrentUser currentUser)
        {
            var userRepository = _storage.GetRepository<User>();
            var keypair = new UserKeyPair(_certificator.GeneratePair());
            var certificate = _certificator.GenerateCertificate(currentUser.FullName, RegisterTokenOptions.ISSUER, keypair.PrivatKey, keypair.PublicKey);
            var user = await userRepository.Get(x => x.Id == currentUser.Id);
            string name = "certificate_" + user.FullName.Replace(" ", "_");
            user.CertificateId = await _fileContext.Save(_certificator.ConvertToByte(certificate), name);
            await userRepository.Update(user);
            FileModel file = new FileModel(Guid.NewGuid() + ".pkf", keypair.PrivatKeyByte);
            return file;
        }

        public async Task<FileModel> GetImage(string id)
        {
            var file = await _fileContext.Get(id);
            if (file == null)
                throw new SignatureException($"Not found image with id {id}", System.Net.HttpStatusCode.NotFound);
            //TO DO:
            //Add checking file for image
            return file;  
        }


 
 
    }
}