using AuthAPI.Data.DTOs;
using AuthAPI.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDTO _response;

        public LicenseController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseDTO>>> GetLicenseTypes()
        {
            try
            {
                var licenses = await _authService.FindAllLicenseTypesAsync();
                _response.Result = licenses;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetLicenseTypeByIdUser(Guid Id)
        {
            try
            {
                var license = await _authService.GetLicenseTypeByIdAsync(Id);
                _response.Result = license;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return Ok(_response);
        }
    }
}
