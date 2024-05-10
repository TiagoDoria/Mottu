using MottuWeb.Models;
using MottuWeb.Service.IService;
using MottuWeb.Utils;

namespace MottuWeb.Service
{
    public class ServiceAuth : IServiceAuth
    {
        private readonly IServiceBase _serviceBase;

        public ServiceAuth(IServiceBase serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public async Task<ResponseDTO?> AssignRoleAsync(RegistrationRequestDTO registrationRequestDTO)
        {
            return await _serviceBase.SendAsync(new RequestDTO()
            {
                ApiType = Configs.ApiType.POST,
                Data = registrationRequestDTO,
                Url = Configs.AuthAPIBase + "/api/auth/AssignRole"
            }, withBearer: false);
        }

        public async Task<ResponseDTO?> GetAllLicenseTypes()
        {
            return await _serviceBase.SendAsync(new RequestDTO()
            {
                ApiType = Configs.ApiType.GET,
                Url = Configs.AuthAPIBase + "/api/auth/LicenseTypes"
            }, withBearer: false);
        }

        public async Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            return await _serviceBase.SendAsync(new RequestDTO()
            {
                ApiType = Configs.ApiType.POST,
                Data = loginRequestDTO,
                Url = Configs.AuthAPIBase + "/api/auth/login"
            }, withBearer: false);
        }

        public async Task<ResponseDTO?> RegisterAsync(RegistrationRequestDTO registrationRequestDTO)
        {
            return await _serviceBase.SendAsync(new RequestDTO()
            {
                ApiType = Configs.ApiType.POST,
                Data = registrationRequestDTO,
                Url = Configs.AuthAPIBase + "/api/auth/register"
            }, withBearer: false);
        }
    }
}
