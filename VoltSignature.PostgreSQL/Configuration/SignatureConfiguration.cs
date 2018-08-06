using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoltSignature.PostgreSQL.Entity;
using VoltSignature.PostgreSQL.Helpers;

namespace VoltSignature.PostgreSQL.Configuration
{
    internal class SignatureConfiguration : DbEntityConfiguration<Signature>
    {
        public override void Configure(EntityTypeBuilder<Signature> entity)
        {
        
        }
    }
}