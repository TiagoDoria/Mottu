using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MottuWeb.Models;
using MottuWeb.Service.IService;
using MottuWeb.Utils;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MottuWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly IServiceAuth _serviceAuth;
        private readonly ITokenProvider _tokenProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _imagePath = Path.Combine("c:", "cnh");

        public AuthController(IServiceAuth serviceAuth, ITokenProvider tokenProvider, IHttpContextAccessor httpContextAccessor)
        {
            _serviceAuth = serviceAuth;
            _tokenProvider = tokenProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginRequestDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO model)
        {
            if (ModelState.IsValid)
            {
                var responseDTO = await _serviceAuth.LoginAsync(model);

                if (responseDTO != null && responseDTO.IsSuccess)
                {
                    var loginResponseDTO = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(responseDTO.Result));
                    await SignInUser(loginResponseDTO);
                    _tokenProvider.SetToken(loginResponseDTO.Token);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("CustomError", responseDTO.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            ViewBag.RoleList = GetRoleList();
            ViewData["LicenseTypes"] = await GetLicenseTypes();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _serviceAuth.RegisterAsync(model);

                if (result != null && result.IsSuccess)
                {
                    if (!string.IsNullOrEmpty(model.Role))
                    {
                        var assignRole = await _serviceAuth.AssignRoleAsync(model);
                        if (assignRole != null && assignRole.IsSuccess)
                        {
                            TempData["success"] = "Registrado com sucesso!";
                            return RedirectToAction(nameof(Login));
                        }
                    }
                }
                else
                {
                    TempData["error"] = result.Message;
                }
            }

            ViewBag.RoleList = GetRoleList();
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(LoginResponseDTO model)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(model.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            AddClaimsToIdentity(jwt, identity);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        private void AddClaimsToIdentity(JwtSecurityToken jwt, ClaimsIdentity identity)
        {
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim("license", jwt.Claims.FirstOrDefault(u => u.Type == "license").Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));
        }

        private List<SelectListItem> GetRoleList()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem{ Text = Configs.RoleAdmin, Value =  Configs.RoleAdmin },
                new SelectListItem{ Text = Configs.RoleDeliveryman, Value =  Configs.RoleDeliveryman }
            };
        }

        private async Task<List<LicenseTypeDTO>> GetLicenseTypes()
        {
            var responseDTO = await _serviceAuth.GetAllLicenseTypes();
            return responseDTO != null && responseDTO.IsSuccess
                ? JsonConvert.DeserializeObject<List<LicenseTypeDTO>>(Convert.ToString(responseDTO.Result))
                : new List<LicenseTypeDTO>();
        }

        public async Task<IActionResult> UploadDriversLicenseImage()
        {
            return View();
        }

        public async Task<IActionResult> UploadDriversLicenseImagePost(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                ModelState.AddModelError("image", "Por favor, selecione um arquivo.");
                return View();
            }

            var allowedExtensions = new[] { ".png", ".bmp" };
            var extension = Path.GetExtension(image.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Apenas imagens nos formatos PNG e BMP são permitidas.");
            }

            var fileId = Guid.NewGuid();
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            string fileName = $"{fileId}{extension}";

            string filePath = Path.Combine(_imagePath, fileName);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            // Salve a imagem no disco
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return Ok($"Imagem salva com sucesso. Caminho: {filePath}");
        }
    }
}
