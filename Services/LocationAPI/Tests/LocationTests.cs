using LocationAPI.Controllers;
using LocationAPI.Data.DTOs;
using LocationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LocationAPI.Tests
{
    public class LocationControllerTestes
    {     

        [Fact]
        public async Task AddAsync_ReturnOkResultDtoNotBull()
        {
            var mockService = new Mock<ILocationService>();
            var controller = new LocationController(mockService.Object);
            var locationDto = new LocationDTO();

            mockService.Setup(service => service.AddAsync(locationDto)).Verifiable();

            var response = await controller.AddAsync(locationDto);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async Task AddAsyncReturnBadRequest_WhenNulll()
        {
            var mockService = new Mock<ILocationService>();
            var controller = new LocationController(mockService.Object);

            var response = await controller.AddAsync(null);

            Assert.IsType<BadRequestResult>(response.Result);
        }

        [Fact]
        public async Task Update_ReturnOkResult_WhenDTONotNull()
        {
            var mockService = new Mock<ILocationService>();
            var controller = new LocationController(mockService.Object);
            var locationDto = new LocationDTO();

            mockService.Setup(service => service.UpdateAsync(locationDto)).Verifiable();

            var resultado = await controller.Update(locationDto);

            Assert.IsType<OkObjectResult>(resultado.Result);
        }

        [Fact]
        public async Task Update_ReturnBadRequest_DtoNotNulll()
        {
            var mockService = new Mock<ILocationService>();
            var controller = new LocationController(mockService.Object);

            var resultado = await controller.Update(null);

            Assert.IsType<BadRequestResult>(resultado.Result);
        }

        [Fact]
        public async Task Delete_ReturnOkResult_IfDelete()
        {
            var mockService = new Mock<ILocationService>();
            var controller = new LocationController(mockService.Object);
            var id = Guid.NewGuid();

            mockService.Setup(service => service.DeleteAsync(id)).ReturnsAsync(true);

            var response = await controller.Delete(id);

            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task Delete_ReturnBadRequest_DeleteAsyncReturnFalse()
        {
            var mockService = new Mock<ILocationService>();
            var controller = new LocationController(mockService.Object);
            var id = Guid.NewGuid();

            mockService.Setup(service => service.DeleteAsync(id)).ReturnsAsync(false);

            var response = await controller.Delete(id);

            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task CalculatePrice_ReturnaOkResult()
        {
            var mockService = new Mock<ILocationService>();
            var controller = new LocationController(mockService.Object);
            var entity = new LocationDTO();

            mockService.Setup(service => service.CalculatePrice(entity)).ReturnsAsync(entity);

            var response = await controller.CalculatePrice(entity);

            Assert.IsType<OkObjectResult>(response);
        }     
    }
}


