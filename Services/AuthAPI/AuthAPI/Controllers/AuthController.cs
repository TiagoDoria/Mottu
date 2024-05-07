using AuthAPI.Data.DTOs;
using AuthAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequestDTO registrationRequestDTO)
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
                var result
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }
    }
}
