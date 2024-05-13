using Microsoft.AspNetCore.Mvc;
using MottuWeb.Models;
using MottuWeb.Service.IService;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MottuWeb.Controllers
{
    public class LocationController : Controller
    {
        private readonly IServiceLocation _serviceLocation;
        private readonly IServiceMotorcycle _serviceMotorcycle;

        public LocationController(IServiceLocation serviceLocation, IServiceMotorcycle serviceMotorcycle)
        {
            _serviceLocation = serviceLocation;
            _serviceMotorcycle = serviceMotorcycle;
        }

        [HttpGet]
        public async Task<IActionResult> IndexLocation()
        {
            var list = await GetLocationsAsync();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> CreateLocation()
        {
            ViewData["Motorcycles"] = await GetAvailableMotorcyclesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation(LocationDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var motorcycle = await UpdateMotorcycleAsync(model.MotorcycleId, false);

                    if (motorcycle != null)
                    {
                        var result = await _serviceLocation.AddLocationAsync(model);

                        if (result != null && result.IsSuccess)
                        {
                            TempData["success"] = "Locação realizada com sucesso!";
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["error"] = result.Message;
                        }
                    }
                }

                ViewData["Motorcycles"] = await GetAvailableMotorcyclesAsync();
                return View();
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Devolution()
        {
            try
            {
                var userId = GetUserId();
                var location = await GetLocationByUserIdAsync(userId);

                if (location == null)
                    return RedirectToAction("Index", "Home");

                var motorcycle = await GetMotorcycleByIdAsync(location.MotorcycleId);

                ViewBag.Motorcycle = motorcycle;
                return View(location);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Pay()
        {
            var location = new LocationDTO { TotalPrice = Convert.ToDecimal(TempData["Price"]) };
            return View(location);
        }

        [HttpPost]
        public async Task<IActionResult> Devolution(LocationDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var motorcycle = await UpdateMotorcycleAsync(Guid.Parse(Request.Form["MotorcycleId"]), true);

                    if (motorcycle != null)
                    {
                        var response = await _serviceLocation.GetTotalValueLocationAsync(model);

                        if (response != null && response.IsSuccess)
                        {
                            var location = JsonConvert.DeserializeObject<LocationDTO>(Convert.ToString(response.Result));
                            TempData["success"] = "Locação finalizada com sucesso!";
                            TempData["Price"] = location.TotalPrice.ToString();
                            return RedirectToAction("Pay");
                        }
                        else
                        {
                            TempData["error"] = response.Message;
                        }
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        private async Task<List<LocationDTO>> GetLocationsAsync()
        {
            var list = new List<LocationDTO>();
            var response = await _serviceLocation.GetAllLocationsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<LocationDTO>>(Convert.ToString(response.Result));
            }

            return list;
        }

        private async Task<List<MotorcycleDTO>> GetAvailableMotorcyclesAsync()
        {
            var list = new List<MotorcycleDTO>();
            var response = await _serviceMotorcycle.GetAvailableMotorcyclesAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<MotorcycleDTO>>(Convert.ToString(response.Result));
            }

            return list;
        }

        private async Task<MotorcycleDTO> UpdateMotorcycleAsync(Guid motorcycleId, bool available)
        {
            var motorcycle = new MotorcycleDTO();
            var response = await _serviceMotorcycle.GetMotorcycleById(motorcycleId);

            if (response != null && response.IsSuccess)
            {
                motorcycle = JsonConvert.DeserializeObject<MotorcycleDTO>(Convert.ToString(response.Result));
                motorcycle.Available = available;
                await _serviceMotorcycle.UpdateMotorcycleAsync(motorcycle);
            }

            return motorcycle;
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.Claims.Where(u => u.Type == ClaimTypes.NameIdentifier)?.FirstOrDefault()?.Value);
        }

        private async Task<LocationDTO> GetLocationByUserIdAsync(Guid userId)
        {
            var location = new LocationDTO();
            var response = await _serviceLocation.GetLocationByIdUserAsync(userId);

            if (response != null && response.IsSuccess)
            {
                location = JsonConvert.DeserializeObject<LocationDTO>(Convert.ToString(response.Result));
            }

            return location;
        }

        private async Task<MotorcycleDTO> GetMotorcycleByIdAsync(Guid motorcycleId)
        {
            var motorcycle = new MotorcycleDTO();
            var response = await _serviceMotorcycle.GetMotorcycleById(motorcycleId);

            if (response != null && response.IsSuccess)
            {
                motorcycle = JsonConvert.DeserializeObject<MotorcycleDTO>(Convert.ToString(response.Result));
            }

            return motorcycle;
        }
    }
}