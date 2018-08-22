using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using VoltSignature.Core.Certificate;

namespace VoltSignature.Test.Certificate
{
    [TestClass]
    public class CertificateTest
    { 

        [TestMethod]
        public void TestCreateCertificate()
        {
            CertificateManager manager = new CertificateManager();
            var KeyPair = manager.GeneratePair();
            var certificate = manager.GenerateCertificate("Tom Holland", "ABC motors", KeyPair.Private, KeyPair.Public);
            Assert.IsNotNull(certificate);
            byte[] certBytes = manager.ConvertToByte(certificate);
            Assert.IsNotNull(certBytes);
            //var cert = build.GenerateKeyValuePair(); 
            File.WriteAllBytes(@"cert.crt", certBytes);

            X509Certificate certificateFromByte = new X509Certificate("cert.crt");
            Assert.IsNotNull(certificateFromByte);

        }
         
    }
}
