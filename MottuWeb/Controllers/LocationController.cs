using Microsoft.AspNetCore.Mvc;
using MottuWeb.Models;
using MottuWeb.Service;
using MottuWeb.Service.IService;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

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
            List<LocationDTO> list = new();
            ResponseDTO responseDTO = await _serviceLocation.GetAllLocationsAsync();
            if (responseDTO != null && responseDTO.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<LocationDTO>>(Convert.ToString(responseDTO.Result));
            }

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> CreateLocation()
        {
            try
            {
                List<MotorcycleDTO> list = new();
                ResponseDTO responseDTO = await _serviceMotorcycle.GetAvailableMotorcyclesAsync();
                if (responseDTO != null && responseDTO.IsSuccess)
                {
                    list = JsonConvert.DeserializeObject<List<MotorcycleDTO>>(Convert.ToString(responseDTO.Result));
                }

                ViewData["Motorcycles"] = list;
                return View();
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation(LocationDTO model)
        {
            try
            {
                MotorcycleDTO motorcycle = new();
                if (ModelState.IsValid)
                {
                    model.UserId = Guid.Parse(User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value);
                    ResponseDTO responseMotorcycle = await _serviceMotorcycle.GetMotorcycleById(model.MotorcycleId);
                    motorcycle = JsonConvert.DeserializeObject<MotorcycleDTO>(Convert.ToString(responseMotorcycle.Result));
                    motorcycle.Available = false;
                    await _serviceMotorcycle.UpdateMotorcycleAsync(motorcycle);


                    ResponseDTO result = await _serviceLocation.AddLocationAsync(model);

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

                List<MotorcycleDTO> list = new();
                ResponseDTO responseDTO = await _serviceMotorcycle.GetAvailableMotorcyclesAsync();
                if (responseDTO != null && responseDTO.IsSuccess)
                {
                    list = JsonConvert.DeserializeObject<List<MotorcycleDTO>>(Convert.ToString(responseDTO.Result));
                }

                ViewData["Motorcycles"] = list;
                return View();
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Devolution()
        {
            try
            {
                var userId = Guid.Parse(User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value);
                LocationDTO location = new();
                ResponseDTO responseDTO = await _serviceLocation.GetLocationByIdUserAsync(userId);
                if (responseDTO != null && responseDTO.IsSuccess)
                {
                    location = JsonConvert.DeserializeObject<LocationDTO>(Convert.ToString(responseDTO.Result));
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


                MotorcycleDTO motorcycle = new();
                ResponseDTO response = await _serviceMotorcycle.GetMotorcycleById(location.MotorcycleId);
                if (response != null && response.IsSuccess)
                {
                    motorcycle = JsonConvert.DeserializeObject<MotorcycleDTO>(Convert.ToString(response.Result));
                }

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
            var userId = Guid.Parse(User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value);
            LocationDTO location = new();
            location.TotalPrice = Convert.ToDecimal(TempData["Price"]);

            return View(location);
        }



        [HttpPost]
        public async Task<IActionResult> Devolution(LocationDTO model)
        {

            try
            {
                MotorcycleDTO motorcycle = new();
                LocationDTO location = new();
                if (ModelState.IsValid)
                {
                    model.UserId = Guid.Parse(User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value);
                    ResponseDTO responseMotorcycle = await _serviceMotorcycle.GetMotorcycleById(Guid.Parse(Request.Form["MotorcycleId"]));
                    motorcycle = JsonConvert.DeserializeObject<MotorcycleDTO>(Convert.ToString(responseMotorcycle.Result));
                    motorcycle.Available = true;
                    await _serviceMotorcycle.UpdateMotorcycleAsync(motorcycle);


                    ResponseDTO response = await _serviceLocation.GetTotalValueLocationAsync(model);
                    if (response != null && response.IsSuccess)
                    {
                        location = JsonConvert.DeserializeObject<LocationDTO>(Convert.ToString(response.Result));
                        TempData["success"] = "Locação finalizada com sucesso!";
                        TempData["Price"] = location.TotalPrice.ToString();
                        return RedirectToAction("Pay");
                    }
                    else
                    {
                        TempData["error"] = response.Message;
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
    }
}
