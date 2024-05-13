using MottuWeb.Models;
using MottuWeb.Service.IService;
using MottuWeb.Utils;

namespace MottuWeb.Service
{
    public class ServiceOrder : IServiceOrder
    {
        private readonly IServiceBase _serviceBase;
        public ServiceOrder(IServiceBase serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public async Task<ResponseDTO?> AddOrderAsync(OrderDTO orderDTO)
        {
            try
            {
                return await _serviceBase.SendAsync(new RequestDTO()
                {
                    ApiType = Configs.ApiType.POST,
                    Data = orderDTO,
                    Url = Configs.OrderAPIBase + "/api/Order/"
                }, withBearer: true);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar pedido!");
            }
        }

        public async Task<ResponseDTO?> DeleteOrderById(Guid id)
        {
            return await _serviceBase.SendAsync(new RequestDTO()
            {
                ApiType = Configs.ApiType.DELETE,
                Url = Configs.OrderAPIBase + "/api/Order/" + id
            }, withBearer: true);
        }

        public async Task<ResponseDTO?> GetAllOrdersAsync()
        {
            return await _serviceBase.SendAsync(new RequestDTO()
            {
                ApiType = Configs.ApiType.GET,
                Url = Configs.OrderAPIBase + "/api/Order"
            }, withBearer: true);
        }

        public async Task<ResponseDTO?> GetOrderById(Guid id)
        {
            return await _serviceBase.SendAsync(new RequestDTO()
            {
                ApiType = Configs.ApiType.GET,
                Url = Configs.OrderAPIBase + "/api/Order/" + id
            }, withBearer: true);
        }

        public async Task<ResponseDTO?> UpdateOrderAsync(OrderDTO orderDTO)
        {
            return await _serviceBase.SendAsync(new RequestDTO()
            {
                ApiType = Configs.ApiType.PUT,
                Data = orderDTO,
                Url = Configs.OrderAPIBase + "/api/Order/"
            }, withBearer: true);
        }
    }
}
