using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VoltSignature.PostgreSQL.Helpers
{
    internal abstract class DbEntityConfiguration<TEntity> where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);
    }
}