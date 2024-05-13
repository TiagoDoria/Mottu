using Microsoft.AspNetCore.Mvc;
using MottuWeb.Models;
using MottuWeb.Service.IService;
using Newtonsoft.Json;

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
    }
}
