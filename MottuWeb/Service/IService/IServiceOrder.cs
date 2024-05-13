using MottuWeb.Models;

namespace MottuWeb.Service.IService
{
    public interface IServiceOrder
    {
        Task<ResponseDTO?> GetOrderById(Guid id);
        Task<ResponseDTO?> GetAllOrdersAsync();
        Task<ResponseDTO?> AddOrderAsync(OrderDTO locationDTO);
        Task<ResponseDTO?> UpdateOrderAsync(OrderDTO locationDTO);
        Task<ResponseDTO?> DeleteOrderById(Guid id);
    }
}
