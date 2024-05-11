using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleAPI.Data.DTOs;
using MotorcycleAPI.Services.interfaces;

namespace MotorcycleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MotorcycleController : ControllerBase
    {
        private IMotorcycleService _service;
        private ResponseDTO _response;
        public MotorcycleController(IMotorcycleService service)
        {
            _service = service;
            _response = new ResponseDTO();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> FindByIdAsync(Guid id)
        {
            try
            {
                var motorcycle = await _service.FindByIdAsync(id);
                if (motorcycle.Id == Guid.Empty) return NotFound();
                _response.Result = motorcycle;             
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }

            return Ok(_response);
        }

        [HttpGet("Available")]
        public async Task<ActionResult<IEnumerable<ResponseDTO>>> FindAvailablesMotorcyclesAsync(Guid id)
        {
            try
            {
                var motorcycles = await _service.FindAvailablesMotorcyclesAsync();
                _response.Result = motorcycles;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }

            return Ok(_response);
        }

        [HttpGet("plate/{plate}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResponseDTO>> FindByPlateAsync(string plate)
        {
            try
            {
                var motorcycle = await _service.FindByPlateAsync(plate);
                if (motorcycle.Id == Guid.Empty) return NotFound();
                _response.Result = motorcycle;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }

            return Ok(_response);
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<IEnumerable<ResponseDTO>>> FindAllAsync()
        {
            try
            {
                var motorcycles = await _service.FindAllAsync();
                _response.Result = motorcycles;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            return Ok(_response);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResponseDTO>> AddAsync([FromBody] MotorcycleDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest();
                await _service.AddAsync(dto);
                _response.Result = dto;
            }
            catch (Exception  e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }

            return Ok(_response);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDTO>> Update([FromBody] MotorcycleDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest();
                await _service.UpdateAsync(dto);
                _response.Result = dto;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }
            
            return Ok(_response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var status = await _service.DeleteAsync(id);
            if (!status) return BadRequest();
            return Ok(status);
        }
    }
}
