using AutoMapper;
using OrderAPI.Data.DTOs;
using OrderAPI.Models;
using OrderAPI.Repository.Interfaces;
using OrderAPI.Services.Interfaces;
using OrderAPI.Utils;

namespace OrderAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(OrderDTO entity)
        {
            try
            {
                entity.Created = DateTime.Now.ToUniversalTime();
                entity.Situation = StatusOrder.Disponivel.ToString();
                await _orderRepository.AddAsync(_mapper.Map<Order>(entity));
            }
            catch (Exception ex) { }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _orderRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<OrderDTO>> FindAllAsync()
        {
            return _mapper.Map<IEnumerable<OrderDTO>>(await _orderRepository.FindAllAsync());
        }

        public async Task<OrderDTO> FindByIdAsync(Guid id)
        {
            return _mapper.Map<OrderDTO>(await _orderRepository.FindByIdAsync(id));
        }
        public async Task UpdateAsync(OrderDTO entity)
        {
            await _orderRepository.UpdateAsync(_mapper.Map<Order>(entity));
        }    
    }
}
