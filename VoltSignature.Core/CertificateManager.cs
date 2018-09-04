using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System; 

namespace VoltSignature.Core
{
    public class CertificateManager
    {
        public CertificateManager()
        {
        }

        //public X509Certificate2 GenerateCertificateForInstall(string subject)
        //{
        //    var random = new SecureRandom();
        //    var certificateGenerator = new X509V3CertificateGenerator();
        //    var serialNumber = BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(Int64.MaxValue), random);
        //    certificateGenerator.SetSerialNumber(serialNumber);
        //    certificateGenerator.SetIssuerDN(new X509Name($"C=NL, O=SomeCompany, CN=Volt signature service"));
        //    certificateGenerator.SetSubjectDN(new X509Name($"C=NL, O=SomeCompany, CN={subject}"));
        //    certificateGenerator.SetNotBefore(DateTime.UtcNow.Date);
        //    certificateGenerator.SetNotAfter(DateTime.UtcNow.Date.AddYears(1));
        //    const int strength = 2048;
        //    var keyGenerationParameters = new KeyGenerationParameters(random, strength);
        //    var keyPairGenerator = new RsaKeyPairGenerator();
        //    keyPairGenerator.Init(keyGenerationParameters);
        //    var subjectKeyPair = keyPairGenerator.GenerateKeyPair();
        //    certificateGenerator.SetPublicKey(subjectKeyPair.Public);
        //    var issuerKeyPair = subjectKeyPair;
        //    const string signatureAlgorithm = "SHA256WithRSA";
        //    var signatureFactory = new Asn1SignatureFactory(signatureAlgorithm, issuerKeyPair.Private);
        //    var bouncyCert = certificateGenerator.Generate(signatureFactory);
        //    Pkcs12Store store = new Pkcs12StoreBuilder().Build();
        //    store.SetKeyEntry($"{subject}_key", new AsymmetricKeyEntry(subjectKeyPair.Private), new[] { new X509CertificateEntry(bouncyCert) });
        //    string exportpw = Guid.NewGuid().ToString("x");
        //    using (var ms = new System.IO.MemoryStream())
        //    {
        //        store.Save(ms, exportpw.ToCharArray(), random);
        //        return new X509Certificate2(ms.ToArray(), exportpw, X509KeyStorageFlags.Exportable);
        //    }
        //}

        public AsymmetricCipherKeyPair GeneratePair()
        {
#if DEBUG
            const int KeyBit = 2048 ;
#else
            const int KeyBit = 8192;
#endif
            RsaKeyPairGenerator generator = new RsaKeyPairGenerator();
            generator.Init(new KeyGenerationParameters(new SecureRandom(), KeyBit));
            AsymmetricCipherKeyPair keyPair = generator.GenerateKeyPair();
            return keyPair;
        }

        public X509Certificate GenerateCertificate(string userFullName, string userCompanyName, AsymmetricKeyParameter issuerPrivate, AsymmetricKeyParameter subjectPublic)
        {
            if (userFullName == null) throw new NullReferenceException("User name cant be null");
            if (userCompanyName == null) throw new NullReferenceException("User company name cant be null");
            ISignatureFactory signatureFactory = new Asn1SignatureFactory(PkcsObjectIdentifiers.Sha256WithRsaEncryption.ToString(), issuerPrivate);
            var certGenerator = new X509V3CertificateGenerator();
            certGenerator.SetIssuerDN(new X509Name($"C=NL, O=Volt signature service"));
            certGenerator.SetSubjectDN(new X509Name($"C=NL, O={userCompanyName}, CN={userFullName}"));
            certGenerator.SetSerialNumber(BigInteger.ValueOf(1));
            certGenerator.SetNotAfter(DateTime.UtcNow.AddYears(1));
            certGenerator.SetNotBefore(DateTime.UtcNow);
            certGenerator.SetPublicKey(subjectPublic); 
            return certGenerator.Generate(signatureFactory);
        }

        public byte[] ConvertToByte(X509Certificate cert)
        {
            if (cert == null) throw new NullReferenceException("Certificate can be null");
            return cert.GetEncoded();
        }
    }
}