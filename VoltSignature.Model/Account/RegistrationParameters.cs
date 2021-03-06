﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VoltSignature.Model.Account
{
    public class RegistrationParameters
    {
        public  string Position { get; set; }
        public string CompanyId { get; set; }
        public string Role { get; set; }


        public const string PositionClaim = "position";
        public const string CompanyClaim = "company";
        public const string RoleClaim = "roleType";
    }
}
