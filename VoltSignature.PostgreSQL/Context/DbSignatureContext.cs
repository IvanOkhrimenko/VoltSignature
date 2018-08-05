using Microsoft.EntityFrameworkCore;
using VoltSignature.PostgreSQL.Configuration;
using VoltSignature.PostgreSQL.Entity;
using VoltSignature.PostgreSQL.Helpers;

namespace VoltSignature.PostgreSQL.Context
{
    public class DbSignatureContext : DbContext
    {
        public DbSignatureContext(DbContextOptions<DbSignatureContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new CompanyConfiguration());
            modelBuilder.AddConfiguration(new SignatureConfiguration());
            modelBuilder.AddConfiguration(new UserConfiguration());
            modelBuilder.AddConfiguration(new UserSignatureConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Signature> Signatures { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserSignature> UserSignatures { get; set; }

    }
}