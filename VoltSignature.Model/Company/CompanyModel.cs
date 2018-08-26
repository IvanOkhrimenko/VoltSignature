using System;
using System.Collections.Generic;
using System.Text;

namespace VoltSignature.Model.Company
{
    public class CompanyModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public DateTime RegisterDate { get; set; }
        public string ImageId { get; set; }
    }
}
