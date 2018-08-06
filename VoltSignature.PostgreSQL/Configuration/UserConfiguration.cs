using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoltSignature.PostgreSQL.Entity;
using VoltSignature.PostgreSQL.Helpers;

namespace VoltSignature.PostgreSQL.Configuration
{
    internal class UserConfiguration : DbEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasOne(x => x.Company).WithMany(x => x.Workers).HasForeignKey(x => x.CompanyId);
            entity.HasOne(x => x.UserRole).WithMany(x => x.Users).HasForeignKey(z => z.RoleId); 
        }
    }
}