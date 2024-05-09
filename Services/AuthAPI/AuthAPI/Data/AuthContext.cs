using AuthAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Data
{
    public class AuthContext : IdentityDbContext<User>
    {
      
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

        public DbSet<User> Deliverymans { get; set; }
        public DbSet<LicenseType> LicenseTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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
