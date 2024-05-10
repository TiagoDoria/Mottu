using LocationAPI.Data.DTOs;
using LocationAPI.Models;
using LocationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocationAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class LocationController : ControllerBase
    {

        private ILocationService _service;
        private ResponseDTO _response;
        public LocationController(ILocationService service)
        {
            _service = service;
            _response = new ResponseDTO();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> FindByIdAsync(Guid id)
        {
            try
            {
                var location = await _service.FindByIdAsync(id);
                if (location.Id == Guid.Empty) return NotFound();
                _response.Result = location;
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
                var locations = await _service.FindAllAsync();
                _response.Result = locations;
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
        public async Task<ActionResult<ResponseDTO>> AddAsync([FromBody] LocationDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest();
                await _service.AddAsync(dto);
                _response.Result = dto;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }

            return Ok(_response);
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResponseDTO>> Update([FromBody] LocationDTO dto)
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
