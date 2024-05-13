using MottuWeb.Models;

namespace MottuWeb.Service.IService
{
    public interface IServiceAuth
    {
        Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO);
        Task<ResponseDTO?> RegisterAsync(RegistrationRequestDTO registrationRequestDTO);
        Task<ResponseDTO?> AssignRoleAsync(RegistrationRequestDTO registrationRequestDTO);
        Task<ResponseDTO?> GetAllLicenseTypes();
        Task<ResponseDTO?> GetLicenseTypeById(Guid id);
    }
}
