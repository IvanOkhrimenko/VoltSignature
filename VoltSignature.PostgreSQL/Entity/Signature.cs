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
        public int SenderId { get; set; }

        public virtual User Sender { get; set; }
        public virtual List<UserSignature> UserSignatures { get; set; }
    }
}