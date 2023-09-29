using AssessmentApi.Data;
using AssessmentApi.Models;
using AssessmentApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssessmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface userInterface;

        public UserController(IUserInterface userInterface)
        {
            this.userInterface = userInterface;
        }


        [HttpPost]
        public async Task<IActionResult> ValidateUserPortalCredentials([FromBody] PortalUser portaluser)
        {
            try
            {
                var result = await userInterface.ValidateCredentials(portaluser);
                if (result == true)
                    return Ok(true);
                return Ok(false);
            }
            catch
            {
                return StatusCode(500, "Error occurred when validating  credentials");
            }
        }





        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserIdByUsername([FromRoute] string username)
        {
            try
            {
                var userId=await userInterface.GetUserId(username);
                return Ok(userId);
            }
            catch
            {
                return StatusCode(500, "Error occurred when fetching userId");
            }
        }

        [HttpGet("validatePolicy/{policynumber}")]
        public async Task<IActionResult> ValidatePolicyNumber([FromRoute] int policynumber)
        {
            try
            {
                var record = await userInterface.ValidatePolicyNumber(policynumber);
                if (record == true) {
                    return Ok(true);
                }
                return Ok(false);


            }
            catch
            {
                return StatusCode(500, "Error occurred when validating  policyNumber");
            }

        }

        [HttpGet("validateChasis/{chasisnumber}")]
        public async Task<IActionResult> ValidateChasisNumber([FromRoute] string chasisnumber)
        {
            try
            {
                var record = await userInterface.ValidateChasisNumber(chasisnumber);
                if (record == true)
                {
                    return Ok(true);
                }
                return Ok(false);


            }
            catch
            {
                return StatusCode(500, "Error occurred when validating  Chasis Number");
            }

        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUserPolicyDetails([FromBody] UserPolicyListDto userPolicyListDto)
        {
            try
            {
                

                var result = await userInterface.AddUserPolicyDetails(userPolicyListDto);
                return Ok(result);

            }
            catch
            {
                return StatusCode(500, "Error occurred when adding UserPolicy list");
            }
        }


        [HttpGet("policyNumber/{UserId}")]
        public async Task<IActionResult?> GetPolicyNumbers([FromRoute]Guid UserId)
        {
            try
            {

                var result = await userInterface.GetPolicyNumbers(UserId);

                if (result == null)
                    return Ok(new int[0]);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Error occurred when fetching Policy Number");

            }
        }

        [HttpGet("InsuredDetails/{policynumber}")]
        public async Task<IActionResult> GetInsuredDetails([FromRoute] int policynumber)
        {
            try
            {
                var record = await userInterface.GetInsuredDetails(policynumber);
                return Ok(record);
            }
            catch
            {
                return StatusCode(500, "Error occurred when fetching Insured Details");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUserPolicyRecord([FromBody]UserPolicyListDto userPolicyListDto)
        {
            try
            {
                var result=await userInterface.DeleteUserPolicy(userPolicyListDto);
                return Ok(result);
            }
            catch 
            {
                return StatusCode(500, "Error occurred when deleting user policy record");
            }
        }








    }
}
