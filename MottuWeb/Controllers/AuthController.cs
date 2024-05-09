using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IdentityModel.Tokens.Jwt;
using MottuWeb.Models;
using MottuWeb.Service.IService;
using MottuWeb.Utils;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MottuWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly IServiceAuth _serviceAuth;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IServiceAuth serviceAuth, ITokenProvider tokenProvider)
        {
            _serviceAuth = serviceAuth;
            _tokenProvider = tokenProvider;
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
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{ Text = Configs.RoleAdmin, Value =  Configs.RoleAdmin },
                new SelectListItem{ Text = Configs.RoleDeliveryman, Value =  Configs.RoleDeliveryman }

            };  

            ViewBag.RoleList = roleList;
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
    }
}
