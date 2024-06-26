﻿using AuthAPI.Data.DTOs;

namespace AuthAPI.Service.Interface
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDTO registrationRequestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<bool> AssignRole(string email, string roleName);
        Task<IEnumerable<LicenseTypeDTO>> FindAllLicenseTypesAsync();
        Task<LicenseTypeDTO> GetLicenseTypeByIdAsync(Guid id);
    }
}
