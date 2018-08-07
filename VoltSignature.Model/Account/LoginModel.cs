using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VoltSignature.Model.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Login is required", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
