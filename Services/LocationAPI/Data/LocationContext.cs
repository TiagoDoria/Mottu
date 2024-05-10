using LocationAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LocationAPI.Data
{
    public class LocationContext : DbContext
    {
        public LocationContext() { }

        public LocationContext(DbContextOptions<LocationContext> options) : base(options) { }

        public DbSet<Location> Locations { get; set; }
    }
}
