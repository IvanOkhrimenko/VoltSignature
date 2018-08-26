using System;
using System.Collections.Generic;
using System.Text;

namespace VoltSignature.Model.User
{
    public class CurrentUser
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName; 
        public string Email { get; set; } 
        public string CompanyId { get; set; }
        public string Role { get; set; }
    }
}
