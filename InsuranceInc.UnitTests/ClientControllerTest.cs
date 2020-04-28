using Xunit;
using Microsoft.AspNetCore.Mvc;
using InsuranceInc.Business.Services;
using InsuranceInc.WebApi.Controllers;

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
            // Act
            var NotFoundResult = _controller.GetClientById("UnknownID");

            // Assert
            Assert.IsType<NotFoundObjectResult>(NotFoundResult.Result);
        }
        
        [Fact]
        public void GetClientById_ExistingIdPassed_ReturnsOkResult()
        {
            // Act
            var OkResult = _controller.GetClientById("a3b8d425-2b60-4ad7-becc-bedf2ef860bd");

            // Assert
            Assert.IsType<OkObjectResult>(OkResult.Result);
        }
        
        [Fact]
        public void GetClientById_ExistingIdPassed_ReturnsRightClient()
        {
            //// Arrange
            //var TestId = "a0ece5db-cd14-4f21-812f-966633e7be86";

            //// Act
            //var OkResult = _controller.GetClientById(TestId).Result as OkObjectResult;

            //// Assert
            //Assert.IsType<string>(OkResult.Value);
            //Assert.Equal(80, (OkResult.Value).ToString().Length);
        }




        //-----------------------------
        // GetClientByName
        //-----------------------------


    }
}