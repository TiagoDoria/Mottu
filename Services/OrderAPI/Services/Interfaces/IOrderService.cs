using OrderAPI.Data.DTOs;

namespace OrderAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddAsync(OrderDTO entity);
        Task UpdateAsync(OrderDTO entity);
        Task<IEnumerable<OrderDTO>> FindAllAsync();
        Task<OrderDTO> FindByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
