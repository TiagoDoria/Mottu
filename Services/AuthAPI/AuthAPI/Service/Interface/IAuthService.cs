using AuthAPI.Data.DTOs;

namespace AuthAPI.Service.Interface
{
    public interface IAuthService
    {
        Task<DeliverymanDTO> Register(RegistrationRequestDTO registrationRequestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
    }
}
