using System.Collections.Generic;
using System.Linq;

namespace VoltSignature.PostgreSQL.Entity
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }

        public virtual List<User> Users { get; set; }
    }
}