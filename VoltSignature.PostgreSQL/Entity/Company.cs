using System;
using System.Collections.Generic;
using System.Linq;

namespace VoltSignature.PostgreSQL.Entity
{
    public class Company
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public int? OwnerId { get; set; }
        public DateTime RegisterDate { get; set; }
        public string ImageId { get; set; }

        public virtual ICollection<User> Workers { get; set; }
        public virtual User Owner { get; set; }
    }
}