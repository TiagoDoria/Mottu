using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Data.DTOs;
using OrderAPI.Services.Interfaces;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private IOrderService _service;
        private ResponseDTO _response;
        public OrderController(IOrderService service)
        {
            _service = service;
            _response = new ResponseDTO();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> FindByIdAsync(Guid id)
        {
            try
            {
                var order = await _service.FindByIdAsync(id);
                if (order.Id == Guid.Empty) return NotFound();
                _response.Result = order;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }

            return Ok(_response);
        }

        [HttpGet]
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
        public async Task<ActionResult<ResponseDTO>> AddAsync([FromBody] OrderDTO dto)
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
        public async Task<ActionResult<ResponseDTO>> Update([FromBody] OrderDTO dto)
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
