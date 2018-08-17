using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace VoltSignature.Core.Exceptions
{
    public class SignatureException : Exception
    {
        public SignatureException(string message, HttpStatusCode code) : base(message)
        {
            StatusCode = code;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
