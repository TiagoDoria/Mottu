using Microsoft.AspNetCore.Mvc;
using Moq;
using MotorcycleAPI.Controllers;
using MotorcycleAPI.Data.DTOs;
using MotorcycleAPI.Services.interfaces;
using Xunit;

namespace MotorcycleAPI.Tests.Controllers
{
    public class MotorcycleControllerTests
    {
        [Fact]
        public async Task FindByIdAsync_ReturnOkObjectResult_MotorcycleExist()
        {
            var mockService = new Mock<IMotorcycleService>();
            var controller = new MotorcycleController(mockService.Object);
            var id = Guid.NewGuid();
            var motorcycleDto = new MotorcycleDTO { Id = id, Year = 2022, Model = "Model X", Plate = "ABC123", Available = true };

            mockService.Setup(service => service.FindByIdAsync(id)).ReturnsAsync(motorcycleDto);

            var resultado = await controller.FindByIdAsync(id);

            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var returnedDto = Assert.IsType<ResponseDTO>(okResult.Value);
            Assert.Equal(motorcycleDto, returnedDto.Result);
        }     

        [Fact]
        public async Task FindAvailablesMotorcyclesAsync_ReturnOkObjectResult_MotorcyclesAvailables()
        {
            var mockService = new Mock<IMotorcycleService>();
            var controller = new MotorcycleController(mockService.Object);
            var motorcycleDtoList = new List<MotorcycleDTO>
            {
                new MotorcycleDTO { Id = Guid.NewGuid(), Year = 2022, Model = "Model A", Plate = "XYZ456", Available = true },
                new MotorcycleDTO { Id = Guid.NewGuid(), Year = 2021, Model = "Model B", Plate = "DEF789", Available = true }
            };

            mockService.Setup(service => service.FindAvailablesMotorcyclesAsync()).ReturnsAsync(motorcycleDtoList);

            var resultado = await controller.FindAvailablesMotorcyclesAsync();

            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var returnedList = Assert.IsType<ResponseDTO>(okResult.Value);
            Assert.Equal(motorcycleDtoList, returnedList.Result);
        }

    }
}
