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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMotorcycle(MotorcycleDTO motorcycleDTO)
        {
            try
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

                return View(motorcycleDTO);
            }
            catch (Exception ex) 
            {
                TempData["error"] = ex.Message;
                return RedirectToAction(nameof(IndexMotorcycle));
            }
        }

        public async Task<IActionResult> UpdateMotorcycle(Guid id)
        {
            ResponseDTO response = await _serviceMotorcycle.GetMotorcycleById(id);
            if (response != null)
            {
                MotorcycleDTO motorcycle = JsonConvert.DeserializeObject<MotorcycleDTO>(Convert.ToString(response.Result));
                return View(motorcycle);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMotorcycle(MotorcycleDTO dto)
        {
            try
            {
                if (dto.Available == false)
                {
                    TempData["error"] = "Não é possível editar moto alugada!";
                    return View(dto);
                }

                if (ModelState.IsValid)
                {
                    ResponseDTO response = await _serviceMotorcycle.UpdateMotorcycleAsync(dto);
                    if (response != null)
                    {
                        return RedirectToAction(nameof(IndexMotorcycle));
                    }
                }

                return View(dto);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction(nameof(IndexMotorcycle));
            }
        }

        public async Task<IActionResult> GetPlate(string searchMotorcycle)
        {
            try
            {
                var responseDTO = await _serviceMotorcycle.GetMotorcycleByPlate(searchMotorcycle);
                var list = new List<MotorcycleDTO>();

                if (responseDTO != null && responseDTO.IsSuccess)
                {
                    var motorcycle = JsonConvert.DeserializeObject<MotorcycleDTO>(Convert.ToString(responseDTO.Result));
                    list.Add(motorcycle);
                }

                return View("IndexMotorcycle", list);
            }
            catch (Exception ex) 
            {
                TempData["error"] = ex.Message;
                return RedirectToAction(nameof(IndexMotorcycle));
            }
           
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

        public async Task<IActionResult> DeleteMotorcycle(Guid id)
        {
            ResponseDTO response = await _serviceMotorcycle.GetMotorcycleById(id);
            if (response != null)
            {
                MotorcycleDTO motorcycle = JsonConvert.DeserializeObject<MotorcycleDTO>(Convert.ToString(response.Result));
                return View(motorcycle);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMotorcycle(MotorcycleDTO dto)
        {
            try
            {
                if (dto.Available == false)
                {
                    TempData["error"] = "Não é possível deletar moto alugada!";
                    return View(dto);
                }

                if (ModelState.IsValid)
                {
                    ResponseDTO response = await _serviceMotorcycle.DeleteMotorcycleById(dto.Id);
                    if (response != null)
                    {
                        return RedirectToAction(nameof(IndexMotorcycle));
                    }
                }

                return View(dto);
            }
           catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction(nameof(IndexMotorcycle));
            }
        }
    }
}
