using Microsoft.AspNetCore.Mvc;
using MottuWeb.Models;
using MottuWeb.Service.IService;
using Newtonsoft.Json;

namespace MottuWeb.Controllers
{
    public class MotorcycleController : Controller
    {
        private readonly IServiceMotorcycle _serviceMotorcycle;

        public MotorcycleController(IServiceMotorcycle serviceMotorcycle)
        {
            _serviceMotorcycle = serviceMotorcycle;
        }

        [HttpGet]
        public async Task<IActionResult> IndexMotorcycle()
        {
            var list = await GetMotorcycleList();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> CreateMotorcycle()
        {
            ViewData["LicenseTypes"] = await GetLicenseTypes();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMotorcycle(MotorcycleDTO motorcycleDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _serviceMotorcycle.AddMotorcycleAsync(motorcycleDTO);

                if (result != null && result.IsSuccess)
                {
                    TempData["success"] = "Locação realizada com sucesso!";
                    return RedirectToAction(nameof(IndexMotorcycle));
                }
                else
                {
                    TempData["error"] = result.Message;
                }
            }

            ViewData["LicenseTypes"] = await GetLicenseTypes();
            return View(motorcycleDTO);
        }

        private async Task<List<MotorcycleDTO>> GetMotorcycleList()
        {
            var list = new List<MotorcycleDTO>();
            var responseDTO = await _serviceMotorcycle.GetAllMotorcyclesAsync();
            if (responseDTO != null && responseDTO.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<MotorcycleDTO>>(Convert.ToString(responseDTO.Result));
            }
            return list;
        }

        private async Task<List<LicenseTypeDTO>> GetLicenseTypes()
        {
            var list = new List<LicenseTypeDTO>();
            var responseDTO = await _serviceMotorcycle.GetAllLicenseTypes();
            if (responseDTO != null && responseDTO.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<LicenseTypeDTO>>(Convert.ToString(responseDTO.Result));
            }
            return list;
        }
    }
}
