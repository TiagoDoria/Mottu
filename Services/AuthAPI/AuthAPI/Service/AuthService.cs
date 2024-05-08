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
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AuthContext db, UserManager<Deliveryman> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.Deliverymans.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.Deliverymans.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
            if (user == null || !isValid)
            {
                return new LoginResponseDTO() { Deliveryman = null, Token= "" };
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            DeliverymanDTO deliverymanDTO = new()
            {
                Id = user.Id,
                Cnpj = user.Cnpj,
                Email = user.Email,
                DriversLicenseNumber = user.DriversLicenseNumber,
                LicenseTypeId = user.LicenseTypeId,
                Name = user.Name,
                BirthDate = user.BirthDate
            };

            LoginResponseDTO responseDTO = new LoginResponseDTO()
            {
                Deliveryman = deliverymanDTO,
                Token = token
            };

            return responseDTO;
        }

        public async Task<string> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            bool status = true;
            Deliveryman user = new()
            {
                UserName = registrationRequestDTO.Email,
                Email = registrationRequestDTO.Email,
                NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
                Name = registrationRequestDTO.Name,
                Cnpj = registrationRequestDTO.Cnpj,
                BirthDate = DateTime.Parse(registrationRequestDTO.BirthDate.ToString()).ToUniversalTime(),
                DriversLicenseNumber = registrationRequestDTO.DriversLicenseNumber,
                LicenseTypeId = registrationRequestDTO.LicenseTypeId
            };

            try
            {
                var checkCnpj = _db.Deliverymans.First(u => u.Cnpj == registrationRequestDTO.Cnpj);
                var checkCnh = _db.Deliverymans.First(u => u.DriversLicenseNumber == registrationRequestDTO.DriversLicenseNumber);
                if (checkCnpj != null || checkCnh != null)
                {
                    status = false;
                }
                var result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);

                if (status && result.Succeeded)
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

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception e)
            {

            }

            if (!status) return "Cnpj field or driver's license number cannot be repeated";

            return "Error Encountered.";
        }
    }
}
