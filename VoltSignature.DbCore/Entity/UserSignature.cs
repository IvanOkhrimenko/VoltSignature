using MongoDB.Bson;

namespace VoltSignature.DbCore.Entity
{
    public class UserSignature
    { 
        public byte[] SignatureHash { get; set; }  
        public string UserId { get; set; }  
    }
}