using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoltSignature.PostgreSQL.Entity;
using VoltSignature.PostgreSQL.Helpers;

namespace VoltSignature.PostgreSQL.Configuration
{
    internal class CompanyConfiguration : DbEntityConfiguration<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> entity)
        {
            entity.HasOne(x=>x.Owner).WithOne(x=>x.OwnCompany).HasForeignKey<Company>(p => p.OwnerId);
            entity.HasMany(x => x.Workers).WithOne(x => x.Company).HasForeignKey(x => x.CompanyId);
        }
    }
}