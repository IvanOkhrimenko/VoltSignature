using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.IO; 
using System.Text;
using VoltSignature.Core;
using VoltSignature.Files.Model;
using VoltSignature.Model.Account; 

namespace VoltSignature.Test
{
    [TestClass]
    public  class CertificateTest
    {
        [TestMethod]
        public void CreateCertificate()
        {
            CertificateManager _certificator = new CertificateManager();
            var keypair = new UserKeyPair(_certificator.GeneratePair());
            var certificate = _certificator.GenerateCertificate("Elton John", RegisterTokenOptions.ISSUER, keypair.PrivatKey, keypair.PublicKey);
            File.WriteAllBytes("testCertificate.crt", _certificator.ConvertToByte(certificate));
        }


        [TestMethod]
        public void ReadCertificate()
        {  
            X509CertificateParser certParser = new X509CertificateParser();
            FileStream fs = new FileStream("testCertificate.crt", FileMode.Open);
            X509Certificate cert = certParser.ReadCertificate(fs);
            fs.Close();
            var publicKeyByte = cert.GetPublicKey() as RsaKeyParameters;
            UserKeyPair pair = new UserKeyPair(publicKeyByte);
        }



        [TestMethod]
        public void TestPrivatKey()
        {
            CertificateManager _certificator = new CertificateManager();
            var keypair = new UserKeyPair(_certificator.GeneratePair());
            var certificate = _certificator.GenerateCertificate("Elton John", RegisterTokenOptions.ISSUER, keypair.PrivatKey, keypair.PublicKey);
            FileModel file = new FileModel(Guid.NewGuid() + ".pkf", keypair.PrivatKeyByte);
            File.WriteAllBytes(file.Name, file.Data);
            file.Data= File.ReadAllBytes(file.Name);
            var newPair = new UserKeyPair(file.Data); 
        }
    }
}
