using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;

namespace VoltSignature.Core
{
    public class UserKeyPair
    {
        public UserKeyPair(AsymmetricCipherKeyPair keyPair)
        {
            if (keyPair == null)
                throw new NullReferenceException("Key value pair cannot be null");
            PrivatKey = (RsaKeyParameters)keyPair.Private;
            PublicKey = (RsaKeyParameters)keyPair.Public;
        }

        public UserKeyPair(RsaKeyParameters publicKey)
        {
            PublicKey = publicKey ?? throw new NullReferenceException("public key value cannot be null");
        }

        public UserKeyPair(byte[] privatKeyByte)
        {
            if (privatKeyByte == null)
                throw new NullReferenceException("private key value cannot be null"); 
            PrivatKey = (RsaKeyParameters)PrivateKeyFactory.CreateKey(privatKeyByte);
        }

        public RsaKeyParameters PrivatKey { get; }
        public RsaKeyParameters PublicKey { get; }

        public byte[] PrivatKeyByte
        {
            get
            {
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