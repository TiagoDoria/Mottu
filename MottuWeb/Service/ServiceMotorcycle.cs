using MottuWeb.Models;
using MottuWeb.Service.IService;
using MottuWeb.Utils;

namespace MottuWeb.Service
{
    public class ServiceMotorcycle : IServiceMotorcycle
    {
        private readonly IServiceBase _serviceBase;
        public ServiceMotorcycle(IServiceBase serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public async Task<ResponseDTO?> AddMotorcycleAsync(MotorcycleDTO motorcycleDTO)
        {
            return await _serviceBase.SendAsync(new RequestDTO()
            {
                ApiType = Configs.ApiType.POST,
                Data = motorcycleDTO,
                Url = Configs.MotorcycleAPIBase + "/api/motorcycle/"
            });
        }
 

        public async Task<ResponseDTO?> DeleteMotorcycleById(Guid id)
        {
            return await _serviceBase.SendAsync(new RequestDTO()
            {
                ApiType = Configs.ApiType.DELETE,
                Url = Configs.MotorcycleAPIBase + "/api/motorcycle/" + id
            });
        }

        public async Task<ResponseDTO?> GetAllMotorcyclesAsync()
        {
            return await _serviceBase.SendAsync(new RequestDTO()
            {
                ApiType = Configs.ApiType.GET,
                Url = Configs.MotorcycleAPIBase + "/api/motorcycle"
            });
        }

        public async Task<ResponseDTO?> GetMotorcycleById(Guid id)
        {
            return await _serviceBase.SendAsync(new RequestDTO()
            {
                ApiType = Configs.ApiType.GET,
                Url = Configs.MotorcycleAPIBase + "/api/motorcycle/" + id
            });
        }

        public async Task<ResponseDTO?> UpdateMotorcycleAsync(MotorcycleDTO motorcycleDTO)
        {
            return await _serviceBase.SendAsync(new RequestDTO()
            {
                ApiType = Configs.ApiType.PUT,
                Data = motorcycleDTO,
                Url = Configs.MotorcycleAPIBase + "/api/motorcycle/"
            });
        }
    }
}
