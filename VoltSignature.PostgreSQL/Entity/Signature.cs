using System;
using System.Collections.Generic;
using System.Linq;

namespace VoltSignature.PostgreSQL.Entity
{
    public class Signature
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string FileId { get; set; } 
        public bool IsAllSignature { get; set; }
         
        public virtual ICollection<UserSignature> UserSignatures { get; set; }
    }
}