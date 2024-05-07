using AuthAPI.Data;
using AuthAPI.Data.DTOs;
using AuthAPI.Models;
using AuthAPI.Service.Interface;
using Microsoft.AspNetCore.Identity;

namespace AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AuthContext _db;
        private readonly UserManager<Deliveryman> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(AuthContext db, UserManager<Deliveryman> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<DeliverymanDTO> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            Deliveryman user = new()
            {
                UserName = registrationRequestDTO.Email,
                Email = registrationRequestDTO.Email,
                NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
                Name = registrationRequestDTO.Name,
                Cnpj = registrationRequestDTO.Cnpj,
                BirthDate = registrationRequestDTO.BirthDate,
                DriversLicenseNumber = registrationRequestDTO.DriversLicenseNumber,
                LicenseTypeId = registrationRequestDTO.LicenseTypeId
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.Deliverymans.First(u => u.UserName == registrationRequestDTO.Email);
                    DeliverymanDTO deliverymanDTO = new()
                    {
                        Id = userToReturn.Id,
                        Cnpj = userToReturn.Cnpj,
                        Email = userToReturn.Email,
                        DriversLicenseNumber = userToReturn.DriversLicenseNumber,
                        LicenseTypeId = userToReturn.LicenseTypeId,
                        Name = userToReturn.Name,
                        BirthDate = userToReturn.BirthDate
                    };

                    return deliverymanDTO;
                }
            }
            catch (Exception e)
            {

                throw;
            }

            return new DeliverymanDTO();
        }
    }
}
