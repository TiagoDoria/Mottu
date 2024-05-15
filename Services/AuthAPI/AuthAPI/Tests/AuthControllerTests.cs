using AuthAPI.Controllers;
using AuthAPI.Data.DTOs;
using AuthAPI.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AuthAPI.Testes.Controladoras
{
    public class AuthTests
    {
        private readonly AuthController _controller;
        private readonly Mock<IAuthService> _authServiceMock;

        public AuthTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _controller = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task TestarRegistro_ComSucesso()
        {
            var registrationRequest = new RegistrationRequestDTO
            {
                Email = "tiago@mail.com",
                Name = "Tiago",
                Password = "Senha@123"
            };

            _authServiceMock.Setup(service => service.Register(It.IsAny<RegistrationRequestDTO>())).ReturnsAsync("");

            var result = await _controller.Register(registrationRequest) as OkObjectResult;

            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public async Task TestarLogin_ComSucesso()
        {
            var loginRequest = new LoginRequestDTO
            {
                UserName = "tiago@mail.com",
                Password = "Senha@123"
            };
            var loginResponse = new LoginResponseDTO
            {
                User = new UserDTO(),
                Token = "descricaodotoken"
            };

            _authServiceMock.Setup(service => 
                service.Login(It.IsAny<LoginRequestDTO>()))
                .ReturnsAsync(loginResponse);

            var resultado = await _controller.Login(loginRequest) as OkObjectResult;

            Assert.NotNull(resultado);
            Assert.True(resultado.StatusCode == 200);
        }

        [Fact]
        public async Task TestarAtribuirRole_ComSucesso()
        {
            var registrationRoleRequest = new RegistrationRoleRequestDTO
            {
                Email = "tiago@mail.com",
                Role = "ADMIN"
            };

            _authServiceMock.Setup(service => 
                service.AssignRole(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            var resultado = await _controller.AssignRole(registrationRoleRequest) as OkObjectResult;

            Assert.NotNull(resultado);
            Assert.True(resultado.StatusCode == 200);
        }
    }
}
