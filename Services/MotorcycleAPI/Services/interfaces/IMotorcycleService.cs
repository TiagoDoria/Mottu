using MotorcycleAPI.Data.DTOs;

namespace MotorcycleAPI.Services.interfaces
{
    public interface IMotorcycleService
    {
        Task AddAsync(MotorcycleDTO entity);
        Task UpdateAsync(MotorcycleUpdateDTO entity);
        Task<IEnumerable<MotorcycleDTO>> FindAllAsync();
        Task<MotorcycleDTO> FindByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<MotorcycleDTO> FindByPlateAsync(string plate);
    }
}
