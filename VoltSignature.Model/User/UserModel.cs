using System;
using System.Collections.Generic;
using System.Text;

namespace VoltSignature.Model.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public DateTime RegisterDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }  
        public string ImageId { get; set; }
        public int CompanyId { get; set; }
        public string Role { get; set; }
    }
}
