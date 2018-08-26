using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace VoltSignature.Core
{
    public  class SignatureManager
    {
        public SignatureManager()
        {
            _signer = SignerUtilities.GetSigner(PkcsObjectIdentifiers.Sha512_256WithRSAEncryption.Id);
        }

        ISigner _signer;

        public void InitPrivateKey(RsaKeyParameters privatKey)
        {
            _signer.Reset();
            _signer.Init(true, privatKey);
        }

        public void InitPublicKey(RsaKeyParameters publicKey)
        {
            _signer.Reset();
            _signer.Init(false, publicKey);
        }
         
        public byte[] SignFile(byte[] file, RsaKeyParameters privatKey)
        {
            _signer.BlockUpdate(file, 0, file.Length);
            byte[] signature = _signer.GenerateSignature();
            return signature;
        }

        public bool VerifySignature(byte[] file, byte[] signature)
        {
            _signer.BlockUpdate(file, 0, file.Length);
            return  _signer.VerifySignature(signature);
        }

        public void Reset() => _signer.Reset();
    }
}
