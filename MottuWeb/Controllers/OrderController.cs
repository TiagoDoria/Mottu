using Microsoft.AspNetCore.Mvc;
using MottuWeb.Models;
using MottuWeb.Service;
using MottuWeb.Service.IService;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace MottuWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly IServiceOrder _serviceOrder;

        public OrderController(IServiceOrder serviceOrder)
        {
            _serviceOrder = serviceOrder;
        }

        [HttpGet]
        public async Task<IActionResult> IndexOrder()
        {
            var list = await GetOrderList();
            return View(list);
        }

        [HttpGet]
        public IActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _serviceOrder.AddOrderAsync(model);

                    if (result != null && result.IsSuccess)
                    {
                        TempData["success"] = "Pedido realizada com sucesso!";
                        return RedirectToAction(nameof(IndexOrder));
                    }
                    else
                    {
                        TempData["error"] = result.Message;
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View(model);
            }
        }

        private async Task<List<OrderDTO>> GetOrderList()
        {
            var list = new List<OrderDTO>();
            var responseDTO = await _serviceOrder.GetAllOrdersAsync();
            if (responseDTO != null && responseDTO.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderDTO>>(Convert.ToString(responseDTO.Result));
            }
            return list;
        }

        [HttpGet]
        public async Task<IActionResult> OrdersAvailables()
        {
            var list = await GetOrderList();
            list = list.Where(x => x.Situation.Equals("Disponivel")).ToList();
            return View(list);
        }

        public async Task<IActionResult> Accept(Guid id)
        {
            try
            {
                OrderDTO dto = new();
                var responseDTO = await _serviceOrder.GetOrderById(id);
                if (responseDTO != null && responseDTO.IsSuccess)
                {
                    dto = JsonConvert.DeserializeObject<OrderDTO>(Convert.ToString(responseDTO.Result));
                }

                if (!dto.Situation.Equals("Disponivel"))
                {
                    TempData["error"] = "Não é possível aceitar o pedido!";
                    return RedirectToAction(nameof(IndexOrder));
                }

                if (ModelState.IsValid)
                {
                    dto.Situation = "Aceito";
                    dto.DeliverymanId = GetUserId();
                    ResponseDTO response = await _serviceOrder.UpdateOrderAsync(dto);
                    if (response != null)
                    {
                        return RedirectToAction(nameof(IndexOrder));
                    }
                }

                return View(dto);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction(nameof(IndexOrder));
            }
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value);
        }
    }
}
