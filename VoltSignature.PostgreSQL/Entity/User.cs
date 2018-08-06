using System;
using System.Collections.Generic;
using System.Linq;

namespace VoltSignature.PostgreSQL.Entity
{
    public class User
    {
        public int Id { get; set; }
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

        public int CompanyId { get; set; }
        public int RoleId { get; set; }

        public virtual Role UserRole { get; set; }
        public virtual Company Company { get; set; }
        public virtual Company OwnCompany { get; set; } 
        public virtual ICollection<UserSignature> UserSignatures { get; set; }
    }
}