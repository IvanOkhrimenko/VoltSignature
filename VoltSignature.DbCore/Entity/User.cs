using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes; 
using System;
using System.Collections.Generic;
using System.Linq;
using VoltSignature.DbCore.Entity.Base;

namespace VoltSignature.DbCore.Entity
{
    [CollectionName("Users")]
    public class User : Base.Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public DateTime RegisterDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public string Password { get; set; }
        public string PublicKey { get; set; }
        public string ImageId { get; set; }
        public string Role { get; set; }
        public string CertificateId { get; set; }

        public string CompanyId { get; set; } 

        

 
        //public virtual Company Company { get; set; }
        //public virtual Company OwnCompany { get; set; } 
        //public virtual ICollection<UserSignature> UserSignatures { get; set; }
    }
}