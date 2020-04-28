using Xunit;
using Microsoft.AspNetCore.Mvc;
using InsuranceInc.Business.Services;
using InsuranceInc.WebApi.Controllers;
using InsuranceInc.Core.Models;

namespace InsuranceInc.UnitTests
{
    public class ClientControllerTest
    {

        IClientService _service;
        ClientController _controller;

        public ClientControllerTest()
        {
            _service = new ClientServiceFake();
            _controller = new ClientController(_service);
        }

        //-----------------------------
        // GetClientById
        //-----------------------------

        [Fact]
        public void GetClientById_NonExistingIdPassed_ReturnsNotFoundResult()
        {
            // Arrange
            var TestId = "UnknownID";

            // Act
            var NotFoundResult = _controller.GetClientById(TestId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(NotFoundResult.Result);
        }
        
        [Fact]
        public void GetClientById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var TestId = "a3b8d425-2b60-4ad7-becc-bedf2ef860bd";

            // Act
            var OkResult = _controller.GetClientById(TestId);

            // Assert
            Assert.IsType<OkObjectResult>(OkResult.Result);
        }
        
        [Fact]
        public void GetClientById_ExistingIdPassed_ReturnsRightClient()
        {
            // Arrange
            var TestId = "a0ece5db-cd14-4f21-812f-966633e7be86";

            // Act
            var OkResult = _controller.GetClientById(TestId).Result as OkObjectResult;

            // Assert
            Assert.IsType<Client>(OkResult.Value);
            Assert.Equal(120, (OkResult.Value).ToString().Length);
        }


        //-----------------------------
        // GetClientByName
        //-----------------------------

        [Fact]
        public void GetClientByName_NonExistingNamePassed_ReturnsNotFoundResult()
        {
            // Arrange
            var TestName = "UnknownID";

            // Act
            var NotFoundResult = _controller.GetClientByName(TestName);

            // Assert
            Assert.IsType<NotFoundObjectResult>(NotFoundResult.Result);
        }

        [Fact]
        public void GetClientByName_ExistingNamePassed_ReturnsOkResult()
        {
            // Arrange
            var TestName = "Jerry";

            // Act
            var OkResult = _controller.GetClientByName(TestName);

            // Assert
            Assert.IsType<OkObjectResult>(OkResult.Result);
        }

        [Fact]
        public void GetClientByName_ExistingNamePassed_ReturnsRightClient()
        {
            // Arrange
            var TestName = "Barnett";

            // Act
            var OkResult = _controller.GetClientByName(TestName).Result as OkObjectResult;

            // Assert
            Assert.Equal(75, (OkResult.Value).ToString().Length);
        }
    }
}
