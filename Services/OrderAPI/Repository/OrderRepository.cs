using Microsoft.EntityFrameworkCore;
using OrderAPI.Data;
using OrderAPI.Models;
using OrderAPI.Repository.Interfaces;

namespace OrderAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public async Task<Order> AddAsync(Order entity)
        {
            _context.Orders.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                Order order =
                await _context.Orders.Where(x => x.Id == id)
                    .FirstOrDefaultAsync() ?? new Order();

                if (order.Id == Guid.Empty) return false;
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<IEnumerable<Order>> FindAllAsync()
        {
            List<Order> orders = await _context.Orders.ToListAsync();
            return orders;
        }

        public async Task<Order> FindByIdAsync(Guid id)
        {
            Order order =
                await _context.Orders.Where(x => x.Id == id)
                .FirstOrDefaultAsync() ?? new Order();
            return order;
        }

        public async Task<Order> UpdateAsync(Order entity)
        {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
