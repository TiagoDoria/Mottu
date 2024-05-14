using MottuWeb.Models;

namespace MottuWeb.Service.IService
{
    public interface IServiceMotorcycle
    {
        Task<ResponseDTO?> GetMotorcycleById(Guid id);
        Task<ResponseDTO?> GetMotorcycleByPlate(string plate);
        Task<ResponseDTO?> GetAllMotorcyclesAsync();
        Task<ResponseDTO?> GetAvailableMotorcyclesAsync();
        Task<ResponseDTO?> AddMotorcycleAsync(MotorcycleDTO motorcycleDTO);
        Task<ResponseDTO?> UpdateMotorcycleAsync(MotorcycleDTO motorcycleDTO);
        Task<ResponseDTO?> DeleteMotorcycleById(Guid id);
    }
}
