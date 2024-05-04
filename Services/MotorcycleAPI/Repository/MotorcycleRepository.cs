using Microsoft.EntityFrameworkCore;
using MotorcycleAPI.Data;
using MotorcycleAPI.Models;
using MotorcycleAPI.Repository.interfaces;

namespace MotorcycleAPI.Repository
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly MotorcycleContext _context;

        public MotorcycleRepository(MotorcycleContext context)
        {
            _context = context;
        }

        public async Task<Motorcycle> AddAsync(Motorcycle entity)
        {
            _context.Motorcycles.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                Motorcycle motorcycle =
                await _context.Motorcycles.Where(x => x.Id == id)
                    .FirstOrDefaultAsync() ?? new Motorcycle();

                if (motorcycle.Id == Guid.Empty) return false;
                _context.Motorcycles.Remove(motorcycle);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<IEnumerable<Motorcycle>> FindAllAsync()
        {
            List<Motorcycle> motorcycles = await _context.Motorcycles.ToListAsync();
            return motorcycles;
        }

        public async Task<Motorcycle> FindByIdAsync(Guid id)
        {
            Motorcycle motorcycle =
                await _context.Motorcycles.Where(x => x.Id == id)
                .FirstOrDefaultAsync() ?? new Motorcycle();
            return motorcycle;
        }

        public async Task<Motorcycle> UpdateAsync(Motorcycle entity)
        {
            _context.Motorcycles.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
