namespace VoltSignature.PostgreSQL.Entity
{
    public class UserSignature
    {
        public int Id { get; set; }
        public byte[] SignatureHash { get; set; }
        public bool IsSender { get; set; }

        public int UserId { get; set; }
        public int SignatureId { get; set; }

        public virtual User User { get; set; }
        public virtual Signature Signature { get; set; }
    }
}