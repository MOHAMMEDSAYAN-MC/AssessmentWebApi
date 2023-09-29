using AssessmentApi.Controllers;
using AssessmentApi.Models;
using AssessmentApi.Services.Interfaces;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace AssessmentApiTest
{
    public class UserCOntrollerTest
    {
        private readonly IFixture fixture;
        private readonly Mock<IUserInterface> userInterface;
        private readonly UserController userController;
        public UserCOntrollerTest()
        {
            fixture = new Fixture();
            fixture.Customize<PortalUser>(composer => composer.Without(t => t.UserPolicies));
            userInterface = fixture.Freeze<Mock<IUserInterface>>();
            userController = new UserController(userInterface.Object);

        }

        //ValidateUserPortalCredentials

        [Fact]
        public async void ValidateUserPortalCredentials_ReturnsOkWithTrue_WhenValidCredentials()
        {
            // Arrange
            var credential = fixture.Create<PortalUser>();
            userInterface.Setup(u => u.ValidateCredentials(credential)).ReturnsAsync(true);

            // Act
            var result = await userController.ValidateUserPortalCredentials(credential);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(true);

            userInterface.Verify(t => t.ValidateCredentials(credential), Times.Once());
        }

        [Fact]
        public async Task ValidateUserPortalCredentials_ReturnsOkWithFalse_WhenInvalidCredentials()
        {
            // Arrange
            var credential = fixture.Create<PortalUser>();
            userInterface.Setup(u => u.ValidateCredentials(credential)).ReturnsAsync(false);

            // Act
            var result = await userController.ValidateUserPortalCredentials(credential);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(false);

            userInterface.Verify(t => t.ValidateCredentials(credential), Times.Once());
        }

        [Fact]
        public async Task ValidateUserPortalCredentials_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var credential = fixture.Create<PortalUser>();
            userInterface.Setup(u => u.ValidateCredentials(credential)).ThrowsAsync(new Exception());

            // Act
            var result = await userController.ValidateUserPortalCredentials(credential);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
            objectResult?.Value.Should().Be("Error occurred when validating  credentials");

            userInterface.Verify(t=>t.ValidateCredentials(credential), Times.Once());
        }



        //GetUserIdByUsername

        [Fact]
        public async Task GetUserIdByUsername_ReturnsOkWithUserId_WhenValidUsername()
        {
            // Arrange
            var username = fixture.Create<string>();
            var userId = fixture.Create<Guid>();
            userInterface.Setup(u => u.GetUserId(username)).ReturnsAsync(userId);

            // Act
            var result = await userController.GetUserIdByUsername(username);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(userId);


            userInterface.Verify(t => t.GetUserId(username), Times.Once());
        }


        [Fact]
        public async Task GetUserIdByUsername_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var username = fixture.Create<string>();
            userInterface.Setup(u => u.GetUserId(username)).ThrowsAsync(new Exception());

            // Act
            var result = await userController.GetUserIdByUsername(username);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
            objectResult?.Value.Should().Be("Error occurred when fetching userId");

            userInterface.Verify(t => t.GetUserId(username), Times.Once());
        }







        //ValidatePolicyNumber
        [Fact]
        public async Task ValidatePolicyNumber_ReturnsOkWithTrue_WhenValidPolicyNumber()
        {
            // Arrange
            var policyNumber = fixture.Create<int>();
            userInterface.Setup(u => u.ValidatePolicyNumber(policyNumber)).ReturnsAsync(true);

            // Act
            var result = await userController.ValidatePolicyNumber(policyNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(true);

            userInterface.Verify(t => t.ValidatePolicyNumber(policyNumber), Times.Once());
        }




        [Fact]
        public async Task ValidatePolicyNumber_ReturnsOkWithFalse_WhenInvalidPolicyNumber()
        {
            // Arrange
            var policyNumber = fixture.Create<int>();
            userInterface.Setup(u => u.ValidatePolicyNumber(policyNumber)).ReturnsAsync(false);

            // Act
            var result = await userController.ValidatePolicyNumber(policyNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(false);

            userInterface.Verify(t => t.ValidatePolicyNumber(policyNumber), Times.Once());
        }

        [Fact]
        public async Task ValidatePolicyNumber_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var policyNumber = fixture.Create<int>();
            userInterface.Setup(u => u.ValidatePolicyNumber(policyNumber)).ThrowsAsync(new Exception());

            // Act
            var result = await userController.ValidatePolicyNumber(policyNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().Be("Error occurred when validating  policyNumber");

            userInterface.Verify(t => t.ValidatePolicyNumber(policyNumber), Times.Once());
        }

        //ValidateChasisNumber

        [Fact]
        public async Task ValidateChasisNumber_ReturnsOkWithTrue_WhenValidChasisNumber()
        {
            // Arrange
            var chasisNumber = fixture.Create<string>();

            userInterface.Setup(u => u.ValidateChasisNumber(chasisNumber)).ReturnsAsync(true);

            // Act
            var result = await userController.ValidateChasisNumber(chasisNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(true);

            userInterface.Verify(t => t.ValidateChasisNumber(chasisNumber), Times.Once());
        }

        [Fact]
        public async Task ValidateChasisNumber_ReturnsOkWithFalse_WhenInvalidChasisNumber()
        {
            // Arrange
            var chasisNumber = fixture.Create<string>();

            userInterface.Setup(u => u.ValidateChasisNumber(chasisNumber)).ReturnsAsync(false);

            // Act
            var result = await userController.ValidateChasisNumber(chasisNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(false);

            userInterface.Verify(t => t.ValidateChasisNumber(chasisNumber), Times.Once());
        }

        [Fact]
        public async Task ValidateChasisNumber_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var chasisNumber = fixture.Create<string>();
            userInterface.Setup(u => u.ValidateChasisNumber(chasisNumber)).ThrowsAsync(new Exception());

            // Act
            var result = await userController.ValidateChasisNumber(chasisNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().Be("Error occurred when validating  Chasis Number");

            userInterface.Verify(t => t.ValidateChasisNumber(chasisNumber), Times.Once());
        }





        //AddUserPolicyDetails
        [Fact]
        public async Task AddUserPolicyDetails_ReturnsOkWithResult_WhenValiduserPolicyrecord()
        {
            // Arrange
            var userpolicyListDto = fixture.Create<UserPolicyListDto>();
            var Result = fixture.Create<bool>();

            userInterface.Setup(u => u.AddUserPolicyDetails(userpolicyListDto)).ReturnsAsync(Result);


            // Act
            var result = await userController.AddUserPolicyDetails(userpolicyListDto);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(Result);


            userInterface.Verify(u => u.AddUserPolicyDetails(userpolicyListDto), Times.Once());
        }

        [Fact]
        public async Task AddUserPolicyDetails_ReturnsInternalServerError_WhenExceptionThrown()
        {
            //Arrange
            var userpolicyListDto = fixture.Create<UserPolicyListDto>();


            userInterface.Setup(u => u.AddUserPolicyDetails(userpolicyListDto)).ThrowsAsync(new Exception());

            // Act
            var result = await userController.AddUserPolicyDetails(userpolicyListDto);


            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
            objectResult?.Value.Should().Be("Error occurred when adding UserPolicy list");

            userInterface.Verify(u => u.AddUserPolicyDetails(userpolicyListDto), Times.Once());
        }



        //GetPolicyNumbers

        [Fact]
        public async Task GetPolicyNumbers_ReturnsOkWithPolicyNumbersArray_WhenPolicyNumbersFound()
        {
            // Arrange
            var policyNumbers = fixture.CreateMany<int>();
            var userId = fixture.Create<Guid>();
            
            userInterface.Setup(u => u.GetPolicyNumbers(userId)).ReturnsAsync(policyNumbers);

            // Act
            var result = await userController.GetPolicyNumbers(userId);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(policyNumbers);

            userInterface.Verify(t => t.GetPolicyNumbers(userId), Times.Once());
        }

        [Fact]
        public async Task GetPolicyNumbers_ReturnsOkWithEmptyArray_WhenNoPolicyNumbersFound()
        {
            // Arrange

            var userId = fixture.Create<Guid>();
  
            userInterface.Setup(u => u.GetPolicyNumbers(userId)).ReturnsAsync((IEnumerable<int>?)null);

            // Act
            var result = await userController.GetPolicyNumbers(userId);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(new int[0]);


            userInterface.Verify(t => t.GetPolicyNumbers(userId), Times.Once());
        }
        [Fact]
        public async Task GetPolicyNumbers_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var userId = fixture.Create<Guid>();
            
            userInterface.Setup(u => u.GetPolicyNumbers(userId)).ThrowsAsync(new Exception());

            // Act
            var result = await userController.GetPolicyNumbers(userId);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().Be("Error occurred when fetching Policy Number");

            userInterface.Verify(t => t.GetPolicyNumbers(userId), Times.Once());

        }

        //GetInsuredDetails

        [Fact]
        public async Task GetInsuredDetails_ReturnsOkWithInsuredDetails_WhenValidPolicyNumber()
        {
            // Arrange
            var policyNumber = fixture.Create<int>();
            var insuredDetails = fixture.Create<object>();
            userInterface.Setup(u => u.GetInsuredDetails(policyNumber)).ReturnsAsync(insuredDetails);

            // Act
            var result = await userController.GetInsuredDetails(policyNumber);

            // Assert
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(insuredDetails);

            userInterface.Verify(t => t.GetInsuredDetails(policyNumber), Times.Once());

        }

        [Fact]
        public async Task GetInsuredDetails_ReturnsInternalServerError_ExceptionThrown()
        {
            // Arrange
            var policyNumber = fixture.Create<int>();
            userInterface.Setup(u => u.GetInsuredDetails(policyNumber)).ThrowsAsync(new Exception());

            // Act
            var result = await userController.GetInsuredDetails(policyNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().Be("Error occurred when fetching Insured Details");


            userInterface.Verify(t => t.GetInsuredDetails(policyNumber), Times.Once());
        }

        //DeleteUserPolicyRecord

        [Fact]
        public async Task DeleteUserPolicyRecord_ReturnsOkWithResult_WhenDeleteSucceeds()
        {
            // Arrange
            var userPolicyListDto = fixture.Create<UserPolicyListDto>();
            var deletionResult = fixture.Create<bool>();

            userInterface.Setup(u => u.DeleteUserPolicy(userPolicyListDto)).ReturnsAsync(deletionResult);

            // Act
            var result = await userController.DeleteUserPolicyRecord(userPolicyListDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().Be(deletionResult);
           
            userInterface.Verify(t => t.DeleteUserPolicy(userPolicyListDto), Times.Once());
        }

        [Fact]
        public async Task DeleteUserPolicyRecord_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var userPolicyListDto = fixture.Create<UserPolicyListDto>();

            userInterface.Setup(u => u.DeleteUserPolicy(userPolicyListDto)).ThrowsAsync(new Exception());

            // Act
            var result = await userController.DeleteUserPolicyRecord(userPolicyListDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("Error occurred when deleting user policy record");

            userInterface.Verify(t => t.DeleteUserPolicy(userPolicyListDto), Times.Once());
        }


    }
}