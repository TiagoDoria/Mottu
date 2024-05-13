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
            List<OrderDTO> list = new();
            ResponseDTO responseDTO = await _serviceOrder.GetAllOrdersAsync();
            if (responseDTO != null && responseDTO.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderDTO>>(Convert.ToString(responseDTO.Result));
            }

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {               
                    ResponseDTO result = await _serviceOrder.AddOrderAsync(model);

                    if (result != null && result.IsSuccess)
                    {
                        TempData["success"] = "Pedido realizada com sucesso!";
                        return RedirectToAction("IndexOrder");
                    }
                    else
                    {
                        TempData["error"] = result.Message;
                    }
                }           
                return View();
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }
    }
}
