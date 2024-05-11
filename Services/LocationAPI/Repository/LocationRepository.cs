using LocationAPI.Data;
using LocationAPI.Models;
using LocationAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocationAPI.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly LocationContext _context;

        public LocationRepository(LocationContext context)
        {
            _context = context;
        }

        public async Task<Location> AddAsync(Location entity)
        {
            _context.Locations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                Location location =
                await _context.Locations.Where(x => x.Id == id)
                    .FirstOrDefaultAsync() ?? new Location();

                if (location.Id == Guid.Empty) return false;
                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<IEnumerable<Location>> FindAllAsync()
        {
            List<Location> locations = await _context.Locations.ToListAsync();
            return locations;
        }

        public async Task<Location> FindByIdAsync(Guid id)
        {
            Location location =
                await _context.Locations.Where(x => x.Id == id)
                .FirstOrDefaultAsync() ?? new Location();
            return location;
        }
        
        public async Task<Location> FindByIdUserAsync(Guid id)
        {
            Location location =
                await _context.Locations.Where(x => x.UserId == id && x.EndDate == null)
                .FirstOrDefaultAsync() ?? new Location();
            return location;
        }

        public async Task<Location> UpdateAsync(Location entity)
        {
            _context.Locations.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
