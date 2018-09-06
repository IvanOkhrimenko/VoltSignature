using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes; 
using System;
using System.Collections.Generic;
using System.Linq;
using VoltSignature.DbCore.Entity.Base;

namespace VoltSignature.DbCore.Entity
{
    [CollectionName("Signatures")]
    public class Signature : Base.Entity
    { 
        public DateTime CreateDate { get; set; }
        public string FileId { get; set; }
        public string AuthorId { get; set; } 
        public string Note { get; set; }
        public List<UserSignature> UserSignatures { get; set; }

        //public virtual ICollection<UserSignature> UserSignatures { get; set; }
    }
}