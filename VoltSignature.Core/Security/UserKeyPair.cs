using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Text;

namespace VoltSignature.Core.Security
{
    public class UserKeyPair
    {
        public UserKeyPair(AsymmetricCipherKeyPair keyPair)
        {
            if (keyPair == null)
                throw new NullReferenceException("Key value pair cannot be null");
            RsaKeyParameters privatKey = (RsaKeyParameters)keyPair.Private;
            RsaKeyParameters publicKey = (RsaKeyParameters)keyPair.Public;
        }

        public UserKeyPair(byte[] publicKeyByte,byte[] privatKeyByte = null)
        {
            if (publicKeyByte == null)
                throw new NullReferenceException("public key value cannot be null");
            PublicKey = (RsaKeyParameters)PublicKeyFactory.CreateKey(publicKeyByte);
            if(privatKeyByte != null)
                PrivatKey = (RsaKeyParameters)PrivateKeyFactory.CreateKey(privatKeyByte);            
        }

        public RsaKeyParameters PrivatKey { get; }
        public RsaKeyParameters PublicKey { get; }

        public byte[] PrivatKeyByte {
            get {
                return PrivateKeyInfoFactory.CreatePrivateKeyInfo(PrivatKey).GetDerEncoded();
            }
        }

        public byte[] PublicKeyByte
        {
            get
            {
                return SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(PublicKey).GetDerEncoded();
            }
        }

    }
}
