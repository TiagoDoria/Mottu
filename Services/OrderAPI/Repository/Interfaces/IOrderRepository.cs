using OrderAPI.Models;

namespace OrderAPI.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> FindAllAsync();
        Task<Order> FindByIdAsync(Guid id);
        Task<Order> AddAsync(Order vo);
        Task<Order> UpdateAsync(Order vo);
        Task<bool> DeleteAsync(Guid id);
    }
}
