using Microsoft.EntityFrameworkCore;
using MotorcycleAPI.Models;

namespace MotorcycleAPI.Data
{
    public class MotorcycleContext : DbContext
    {
        public MotorcycleContext() { }

        public MotorcycleContext(DbContextOptions<MotorcycleContext> options) : base(options) { }

        public DbSet<Motorcycle> Motorcycles { get; set; }


    }
}
