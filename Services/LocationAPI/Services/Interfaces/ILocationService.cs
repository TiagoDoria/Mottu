using LocationAPI.Data.DTOs;

namespace LocationAPI.Services.Interfaces
{
    public interface ILocationService
    {
        Task AddAsync(LocationDTO entity);
        Task UpdateAsync(LocationDTO entity);
        Task<IEnumerable<LocationDTO>> FindAllAsync();
        Task<LocationDTO> FindByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<LocationDTO> CalculatePrice(LocationDTO entity);
    }
}
