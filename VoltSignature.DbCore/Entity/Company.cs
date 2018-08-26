using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes; 
using System;
using System.Collections.Generic;
using System.Linq;
using VoltSignature.DbCore.Entity.Base;

namespace VoltSignature.DbCore.Entity
{
    [CollectionName("Companys")]
    public class Company : Base.Entity
    { 
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public DateTime RegisterDate { get; set; }
        public string ImageId { get; set; }
 
    }
}