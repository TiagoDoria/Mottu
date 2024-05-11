using LocationAPI.Models;

namespace LocationAPI.Repository.Interfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> FindAllAsync();
        Task<Location> FindByIdAsync(Guid id);
        Task<Location> FindByIdUserAsync(Guid id);
        Task<Location> AddAsync(Location vo);
        Task<Location> UpdateAsync(Location vo);
        Task<bool> DeleteAsync(Guid id);
    }
}
