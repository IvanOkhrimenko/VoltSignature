using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoltSignature.PostgreSQL.Entity;
using VoltSignature.PostgreSQL.Helpers;

namespace VoltSignature.PostgreSQL.Configuration
{
    internal class UserSignatureConfiguration : DbEntityConfiguration<UserSignature>
    {
        public override void Configure(EntityTypeBuilder<UserSignature> entity)
        {
            entity.HasOne(sc => sc.Signature).WithMany(s => s.UserSignatures).HasForeignKey(sc => sc.SignatureId);
            entity.HasOne(sc => sc.User).WithMany(c => c.UserSignatures).HasForeignKey(sc => sc.UserId);
        }
    }
}