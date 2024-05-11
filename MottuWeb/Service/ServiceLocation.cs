using MottuWeb.Models;
using MottuWeb.Service.IService;
using MottuWeb.Utils;

namespace MottuWeb.Service
{
    public class ServiceLocation : IServiceLocation
    {
        private readonly IServiceBase _serviceBase;
        private readonly IServiceMotorcycle _serviceMotorcycle;
        public ServiceLocation(IServiceBase serviceBase, IServiceMotorcycle serviceMotorcycle)
        {
            _serviceBase = serviceBase;
            _serviceMotorcycle = serviceMotorcycle;
        }

        public async Task<ResponseDTO?> AddLocationAsync(LocationDTO locationDTO)
        {
            try
            {
                return await _serviceBase.SendAsync(new RequestDTO()
                {
                    ApiType = Configs.ApiType.POST,
                    Data = locationDTO,
                    Url = Configs.LocationAPIBase + "/api/Location/"
                }, withBearer: true);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar locação!");
            }
        }
        public async Task<ResponseDTO?> GetTotalValueLocationAsync(LocationDTO locationDTO)
        {
            try
            {
                return await _serviceBase.SendAsync(new RequestDTO()
                {
                    ApiType = Configs.ApiType.POST,
                    Data = locationDTO,
                    Url = Configs.LocationAPIBase + "/api/Location/calculate"
                }, withBearer: true);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao calcular valor da locação!");
            }
        }


        public async Task<ResponseDTO?> DeleteLocationById(Guid id)
        {
            return await _serviceBase.SendAsync(new RequestDTO()
            {
                ApiType = Configs.ApiType.DELETE,
                Url = Configs.LocationAPIBase + "/api/Location/" + id
            }, withBearer: true);
        }

        public async Task<ResponseDTO?> GetAllLocationsAsync()
        {
            return await _serviceBase.SendAsync(new RequestDTO()
            {
                ApiType = Configs.ApiType.GET,
                Url = Configs.LocationAPIBase + "/api/Location"
            }, withBearer: true);
        }

        public async Task<ResponseDTO?> GetLocationById(Guid id)
        {
            return await _serviceBase.SendAsync(new RequestDTO()
            {
                ApiType = Configs.ApiType.GET,
                Url = Configs.LocationAPIBase + "/api/Location/" + id
            }, withBearer: true);
        }
        public async Task<ResponseDTO?> GetLocationByIdUserAsync(Guid id)
        {
            return await _serviceBase.SendAsync(new RequestDTO()
            {
                ApiType = Configs.ApiType.GET,
                Url = Configs.LocationAPIBase + "/api/Location/LocationActive/" + id
            }, withBearer: true);
        }

        public async Task<ResponseDTO?> UpdateLocationAsync(LocationDTO locationDTO)
        {
            return await _serviceBase.SendAsync(new RequestDTO()
            {
                ApiType = Configs.ApiType.PUT,
                Data = locationDTO,
                Url = Configs.LocationAPIBase + "/api/Location/"
            }, withBearer: true);
        }
    }
}
