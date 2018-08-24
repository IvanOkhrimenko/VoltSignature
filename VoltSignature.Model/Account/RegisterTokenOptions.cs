using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace VoltSignature.Model.Account
{
    public class RegisterTokenOptions
    {
        public const string ISSUER = "VoltSignatureService"; // издатель токена
        public const string AUDIENCE = "http://localhost:51884/"; // потребитель токена
        const string KEY = "7e800774-8e03-434a-a658-7642aea283f4";   // ключ для шифрации
        public const int LIFETIME = 20; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
