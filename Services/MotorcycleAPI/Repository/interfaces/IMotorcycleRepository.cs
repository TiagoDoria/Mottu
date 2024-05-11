using MotorcycleAPI.Models;

namespace MotorcycleAPI.Repository.interfaces
{
    public interface IMotorcycleRepository
    {
        Task<IEnumerable<Motorcycle>> FindAllAsync();
        Task<Motorcycle> FindByIdAsync(Guid id);
        Task<Motorcycle> AddAsync(Motorcycle vo);
        Task<Motorcycle> UpdateAsync(Motorcycle vo);
        Task<bool> DeleteAsync(Guid id);
        Task<Motorcycle> FindByPlateAsync(string plate);
        Task<IEnumerable<Motorcycle>> FindAvailablesMotorcyclesAsync();
    }
}
