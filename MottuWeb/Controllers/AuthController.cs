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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _imagePath = Path.Combine("c:", "cnh");
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostingEnvironment;



        public AuthController(IServiceAuth serviceAuth, ITokenProvider tokenProvider, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment)
        {
            _serviceAuth = serviceAuth;
            _tokenProvider = tokenProvider;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDTO loginRequestDTO = new LoginRequestDTO();
            return View(loginRequestDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO model)
        {    
            if (ModelState.IsValid)
            {
                ResponseDTO responseDTO = await _serviceAuth.LoginAsync(model);

                if (responseDTO != null && responseDTO.IsSuccess)
                {
                    LoginResponseDTO loginResponseDTO = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(responseDTO.Result));
                    await SignInUser(loginResponseDTO);
                    _tokenProvider.SetToken(loginResponseDTO.Token);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("CustomError", responseDTO.Message);
                    return View(model);
                }
            }
    
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            List<LicenseTypeDTO> list = new();
            ResponseDTO responseDTO = await _serviceAuth.GetAllLicenseTypes();
            if (responseDTO != null && responseDTO.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<LicenseTypeDTO>>(Convert.ToString(responseDTO.Result));
            }

            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{ Text = Configs.RoleAdmin, Value =  Configs.RoleAdmin },
                new SelectListItem{ Text = Configs.RoleDeliveryman, Value =  Configs.RoleDeliveryman }

            };  

            ViewBag.RoleList = roleList;
            ViewData["LicenseTypes"] = list;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDTO model)
        {
            if (string.IsNullOrEmpty(model.Role))
            {
                ModelState.AddModelError("Role", "The Role field is required.");
            }
            if (ModelState.IsValid)
            {
                ResponseDTO result = await _serviceAuth.RegisterAsync(model);
                ResponseDTO assignRole;

                if (result != null && result.IsSuccess)
                {
                    if (!string.IsNullOrEmpty(model.Role))
                    {
                        assignRole = await _serviceAuth.AssignRoleAsync(model);
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

            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{ Text = Configs.RoleAdmin, Value =  Configs.RoleAdmin },
                new SelectListItem{ Text = Configs.RoleDeliveryman, Value =  Configs.RoleDeliveryman }

            };

            ViewBag.RoleList = roleList;
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
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal); 
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

            var FileId = Guid.NewGuid();
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            string fileName = $"{FileId}{extension}";

            string filePath = Path.Combine(_imagePath);

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
