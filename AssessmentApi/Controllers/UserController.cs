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
        public async Task<IActionResult> ValidateCredentials([FromBody] PortalUser portaluser)
        {
            try
            {
                var rec = await userInterface.validateCredentials(portaluser);
                if (rec == true)
                    return Ok(true);
                return Ok(false);
            }
            catch
            {
                return StatusCode(500, "Error occurred when validating  credentials");
            }
        }

        [HttpGet("validatePolicy/{policynumber}")]
        public async Task<IActionResult> ValidatePolicyNumber([FromRoute] int policynumber)
        {
            try
            {
                var record = await userInterface.validatePolicyNumber(policynumber);
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
                var record = await userInterface.validateChasisNumber(chasisnumber);
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
                var result = await userInterface.addUserPolicyDetails(userPolicyListDto);
                return Ok(result);

            }
            catch
            {
                return StatusCode(500, "Error occurred when adding UserPolicy list");
            }
        }


        [HttpGet("policyNumber")]
        public async Task<IActionResult> GetPolicyNumbers()
        {
            try
            {
                var result = await userInterface.getPolicyNumbers();
                if (result == null)
                    return Ok(new { message = "No Content" });
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
                var record = await userInterface.getInsuredDetails(policynumber);
                return Ok(record);
            }
            catch
            {
                return StatusCode(500, "Error occurred when fetching Insured Details");
            }
        }
        [HttpDelete("{policynumber}")]
        public async Task<IActionResult> DeleteUserPolicyRecord([FromRoute] int policynumber)
        {
            try
            {
                var result=await userInterface.deleteUserPolicy(policynumber);
                return Ok(result);
            }
            catch 
            {
                return StatusCode(500, "Error occurred when deleting user policy record");
            }
        }








    }
}
