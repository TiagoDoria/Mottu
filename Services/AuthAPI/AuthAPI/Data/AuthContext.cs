using AuthAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Data
{
    public class AuthContext : IdentityDbContext<Deliveryman>
    {
      
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

        public DbSet<Deliveryman> Deliverymans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deliveryman>(entity => {
                entity.HasIndex(e => e.Cnpj).IsUnique();
                entity.HasIndex(e => e.DriversLicenseNumber).IsUnique();
            });

            modelBuilder.Entity<LicenseType>().HasData(new LicenseType
            {
                Description = "A"
            });
            modelBuilder.Entity<LicenseType>().HasData(new LicenseType
            {
                Description = "B"
            });
            modelBuilder.Entity<LicenseType>().HasData(new LicenseType
            {
                Description = "A+B"
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
