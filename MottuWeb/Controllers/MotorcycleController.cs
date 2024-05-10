using Microsoft.AspNetCore.Mvc;
using MottuWeb.Models;
using MottuWeb.Service.IService;
using Newtonsoft.Json;
using System.Collections.Generic;

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
            List<MotorcycleDTO> list = new();
            ResponseDTO responseDTO = await _serviceMotorcycle.GetAllMotorcyclesAsync();
            if (responseDTO != null && responseDTO.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<MotorcycleDTO>>(Convert.ToString(responseDTO.Result));
            }

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> CreateMotorcycle()
        {
            List<LicenseTypeDTO> list = new();
            ResponseDTO responseDTO = await _serviceMotorcycle.GetAllLicenseTypes();
            if (responseDTO != null && responseDTO.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<LicenseTypeDTO>>(Convert.ToString(responseDTO.Result));
            }
            
            ViewData["LicenseTypes"] = list;

            return View();
        }

    }
}
