using System;
using System.Collections.Generic;
using System.Text;

namespace VoltSignature.Model.Signature
{
    public class SignatureModel
    {
        public string AuthorId { get; set; }
        public string FileId { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuthorFullName { get; set; }
        public string AuthorEmail { get; set; }
        public string ImageId { get; set; }
        public string FileName { get; set; }
    }
}
