using MottuWeb.Models;

namespace MottuWeb.Service.IService
{
    public interface IServiceLocation
    {
        Task<ResponseDTO?> GetLocationById(Guid id);
        Task<ResponseDTO?> GetAllLocationsAsync();
        Task<ResponseDTO?> AddLocationAsync(LocationDTO locationDTO);
        Task<ResponseDTO?> UpdateLocationAsync(LocationDTO locationDTO);
        Task<ResponseDTO?> DeleteLocationById(Guid id);
        Task<ResponseDTO?> GetLocationByIdUserAsync(Guid id);
        Task<ResponseDTO?> GetTotalValueLocationAsync(LocationDTO locationDTO);
    }
}
