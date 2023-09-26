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

        //ValidateCredentials

        [Fact]
        public async void ValidateCredentials_ValidCredentials_ReturnsTrue()
        {
            // Arrange
            var credential = fixture.Create<PortalUser>();
            userInterface.Setup(u => u.validateCredentials(credential)).ReturnsAsync(true);

            // Act
            var result = await userController.ValidateCredentials(credential);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(true);

            userInterface.Verify(t => t.validateCredentials(credential), Times.Once());
        }

        [Fact]
        public async Task ValidateCredentials_InvalidCredentials_ReturnsFalse()
        {
            // Arrange
            var credential = fixture.Create<PortalUser>();
            userInterface.Setup(u => u.validateCredentials(credential)).ReturnsAsync(false);

            // Act
            var result = await userController.ValidateCredentials(credential);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(false);

            userInterface.Verify(t => t.validateCredentials(credential), Times.Once());
        }

        [Fact]
        public async Task ValidateCredentials_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var credential = fixture.Create<PortalUser>();
            userInterface.Setup(u => u.validateCredentials(credential)).ThrowsAsync(new Exception());

            // Act
            var result = await userController.ValidateCredentials(credential);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("Error occurred when validating  credentials");

            userInterface.Verify(t=>t.validateCredentials(credential), Times.Once());
        }

        //ValidatePolicyNumber
        [Fact]
        public async Task ValidatePolicyNumber_WhenValidPolicyNumber_ReturnsTrue()
        {
            // Arrange
            var policyNumber = fixture.Create<int>();
            userInterface.Setup(u => u.validatePolicyNumber(policyNumber)).ReturnsAsync(true);

            // Act
            var result = await userController.ValidatePolicyNumber(policyNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(true);

            userInterface.Verify(t => t.validatePolicyNumber(policyNumber), Times.Once());
        }




        [Fact]
        public async Task ValidatePolicyNumber_WhenInvalidPolicyNumber_ReturnsFalse()
        {
            // Arrange
            var policyNumber = fixture.Create<int>();
            userInterface.Setup(u => u.validatePolicyNumber(policyNumber)).ReturnsAsync(false);

            // Act
            var result = await userController.ValidatePolicyNumber(policyNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(false);

            userInterface.Verify(t => t.validatePolicyNumber(policyNumber), Times.Once());
        }

        [Fact]
        public async Task ValidatePolicyNumber_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var policyNumber = fixture.Create<int>();
            userInterface.Setup(u => u.validatePolicyNumber(policyNumber)).ThrowsAsync(new Exception());

            // Act
            var result = await userController.ValidatePolicyNumber(policyNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().Be("Error occurred when validating  policyNumber");

            userInterface.Verify(t => t.validatePolicyNumber(policyNumber), Times.Once());
        }
        //ValidateChasisNumber

        [Fact]
        public async Task ValidateChasisNumber_WhenValidChasisNumber_ReturnsTrue()
        {
            // Arrange
            var chasisNumber = fixture.Create<string>();

            userInterface.Setup(u => u.validateChasisNumber(chasisNumber)).ReturnsAsync(true);

            // Act
            var result = await userController.ValidateChasisNumber(chasisNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(true);

            userInterface.Verify(t => t.validateChasisNumber(chasisNumber), Times.Once());
        }

        [Fact]
        public async Task ValidateChasisNumber_WhenInvalidChasisNumber_ReturnsFalse()
        {
            // Arrange
            var chasisNumber = fixture.Create<string>();

            userInterface.Setup(u => u.validateChasisNumber(chasisNumber)).ReturnsAsync(false);

            // Act
            var result = await userController.ValidateChasisNumber(chasisNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(false);

            userInterface.Verify(t => t.validateChasisNumber(chasisNumber), Times.Once());
        }

        [Fact]
        public async Task ValidateChasisNumber_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var chasisNumber = fixture.Create<string>();
            userInterface.Setup(u => u.validateChasisNumber(chasisNumber)).ThrowsAsync(new Exception());

            // Act
            var result = await userController.ValidateChasisNumber(chasisNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().Be("Error occurred when validating  Chasis Number");

            userInterface.Verify(t => t.validateChasisNumber(chasisNumber), Times.Once());
        }





        //AddUserPolicyDetails
        [Fact]
        public async Task AddUserPolicyDetails_ValiduserPolicyrecords_ReturnsOkWithResult()
        {
            // Arrange
            var userpolicyList = fixture.Create<UserPolicyListDto>();
            var Result = fixture.Create<bool>();
            userInterface.Setup(u => u.addUserPolicyDetails(userpolicyList)).ReturnsAsync(Result);

            // Act
            var result = await userController.AddUserPolicyDetails(userpolicyList);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(Result);

            userInterface.Verify(t => t.addUserPolicyDetails(userpolicyList), Times.Once());
        }

        [Fact]
        public async Task AddUserPolicyDetailsd_ExceptionThrown_ReturnsInternalServerError()
        {
            //Arrange
            var userpolicyList = fixture.Create<UserPolicyListDto>();
            var Result = fixture.Create<bool>();
            userInterface.Setup(u => u.addUserPolicyDetails(userpolicyList)).ThrowsAsync(new Exception());

            // Act
            var result = await userController.AddUserPolicyDetails(userpolicyList);

          
            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("Error occurred when adding UserPolicy list");

            userInterface.Verify(t => t.addUserPolicyDetails(userpolicyList), Times.Once());
        }



        //GetPolicyNumbers

        [Fact]
        public async Task GetPolicyNumbers_ReturnsPolicyNumbers_WhenResultIsNotNull()
        {
            // Arrange
            var policyNumbers = fixture.CreateMany<int>();
            userInterface.Setup(u => u.getPolicyNumbers()).ReturnsAsync(policyNumbers);

            // Act
            var result = await userController.GetPolicyNumbers();

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(policyNumbers);

            userInterface.Verify(t => t.getPolicyNumbers(), Times.Once());
        }

        [Fact]
        public async Task GetPolicyNumbers_WhenEmptyData_ReturnsOkWithMessage()
        {
            // Arrange
            userInterface.Setup(u => u.getPolicyNumbers()).ReturnsAsync((List<int>)null); 

            // Act
            var result = await userController.GetPolicyNumbers();

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(new { message = "No Content" });

            userInterface.Verify(t => t.getPolicyNumbers(), Times.Once());
        }
        [Fact]
        public async Task GetPolicyNumbers_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            userInterface.Setup(u => u.getPolicyNumbers()).ThrowsAsync(new Exception());

            // Act
            var result = await userController.GetPolicyNumbers();

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().Be("Error occurred when fetching Policy Number");

            userInterface.Verify(t => t.getPolicyNumbers(), Times.Once());

        }

        //GetInsuredDetails

        [Fact]
        public async Task GetInsuredDetails_ValidData_ReturnsOkWithResult()
        {
            // Arrange
            var policyNumber = fixture.Create<int>(); 
            var insuredDetails = fixture.Create<object>(); 
            userInterface.Setup(u => u.getInsuredDetails(policyNumber)).ReturnsAsync(insuredDetails);

            // Act
            var result = await userController.GetInsuredDetails(policyNumber);

            // Assert
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(insuredDetails);

            userInterface.Verify(t => t.getInsuredDetails(policyNumber), Times.Once());

        }

        [Fact]
        public async Task GetInsuredDetails_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var policyNumber = fixture.Create<int>();
            userInterface.Setup(u => u.getInsuredDetails(policyNumber)).ThrowsAsync(new Exception());

            // Act
            var result = await userController.GetInsuredDetails(policyNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().Be("Error occurred when fetching Insured Details");


            userInterface.Verify(t => t.getInsuredDetails(policyNumber), Times.Once());
        }

        //DeleteUserPolicyRecord

        [Fact]
        public async Task DeleteUserPolicyRecord_ValidPolicyNumber_ReturnsOkWithResult()
        {
            // Arrange
            var policyNumber = fixture.Create<int>(); 
            var deletionResult = fixture.Create<bool>(); 
            userInterface.Setup(u => u.deleteUserPolicy(policyNumber)).ReturnsAsync(deletionResult);

            // Act
            var result = await userController.DeleteUserPolicyRecord(policyNumber);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IActionResult>();
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(deletionResult);

            userInterface.Verify(t => t.deleteUserPolicy(policyNumber), Times.Once());
        }

        [Fact]
        public async Task DeleteUserPolicyRecord_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var policyNumber = fixture.Create<int>();
            userInterface.Setup(u => u.deleteUserPolicy(policyNumber)).ThrowsAsync(new Exception());

            // Act
            var result = await userController.DeleteUserPolicyRecord(policyNumber);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("Error occurred when deleting user policy record");

            userInterface.Verify(t => t.deleteUserPolicy(policyNumber), Times.Once());
        }


    }
}