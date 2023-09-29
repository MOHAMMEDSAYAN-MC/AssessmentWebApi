using AssessmentApi.Data;
using AssessmentApi.Models;
using AssessmentApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AssessmentApi.Services
{
    public class UserRepository : IUserInterface
    {
        private readonly DataContext dataContext;
        private readonly ServerContext serverContext;

        public UserRepository(DataContext dataContext,ServerContext serverContext)
        {
            this.dataContext = dataContext;
            this.serverContext = serverContext;
        }

        public async Task<bool> AddUserPolicyDetails(UserPolicyListDto userPolicyListDto)
        {
            var existingRecord = await dataContext.userpolicylist.FirstOrDefaultAsync(u => u.PolicyNumber ==userPolicyListDto.PolicyNumber && u.UserId==userPolicyListDto.UserId);

            if (existingRecord != null)
            {
                return false;
            }
            var result = new UserPolicyList
            {
                Id = Guid.NewGuid(),
                UserId = userPolicyListDto.UserId,
                PolicyNumber = userPolicyListDto.PolicyNumber

            };
            await dataContext.userpolicylist.AddAsync(result);
            await dataContext.SaveChangesAsync();
            return true;

        }

        public async Task<bool> DeleteUserPolicy(UserPolicyListDto userPolicyListDto)
        {
            var record= await dataContext.userpolicylist.FirstOrDefaultAsync(u=>u.PolicyNumber == userPolicyListDto.PolicyNumber && u.UserId == userPolicyListDto.UserId);
            if(record == null)
            {
                return false;
            }
            dataContext.userpolicylist.Remove(record);
            await dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<object?> GetInsuredDetails(int policynumber)
        {
           

            var policyDetails = await serverContext.Policies
                .Where(pol => pol.PolicyNumber == policynumber)
                .Join(
                    serverContext.Policyinsureds,
                    pol => pol.PolicyId,
                    polins => polins.PolicyId,
                    (pol, polins) => new { Policy = pol, PolicyInsured = polins })
                    .FirstOrDefaultAsync();
            if (policyDetails == null)
            {
                return null;
            }
            var insuredDetails = await serverContext.Insureds
                .Where(ins => ins.InsuredId == policyDetails.PolicyInsured.InsuredId)
                .Join(
                        serverContext.Contacts,
                        ins => ins.ContactId,
                        con => con.ContactId,
                        (ins, con) => new
                        {
                            con.FirstName,
                            con.LastName,
                            con.AddressLine1,
                            con.AddressLine2,
                            con.City,
                            con.State,
                            con.Pincode,
                            con.MobileNo,
                            con.Email,
                            ins.AadharNumber,
                            ins.LicenseNumber,
                            ins.Pannumber, 
                            ins.AccountNumber,
                            ins.Ifsccode, 
                            ins.BankName,
                            ins.BankAddress
                         }).FirstOrDefaultAsync();


            return insuredDetails;

        }

        public async Task<IEnumerable<int>?> GetPolicyNumbers(Guid userId)
        {
            var result = await dataContext.userpolicylist
                .Where(up => up.UserId == userId)
                .Select(up => up.PolicyNumber)
                .ToListAsync();
            if (result.Any())
            {
                return result;
            }
            return null;
        }

        public async Task<bool> ValidateCredentials(PortalUser portalUser)
        {
            var record = await dataContext.portaluser.FirstOrDefaultAsync(a => a.UserName == portalUser.UserName && a.Password == portalUser.Password);
            if (record == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ValidateChasisNumber(string chasisnumber)
        {
            var record = await serverContext.Vehicles.FirstOrDefaultAsync(v => v.ChasisNumber == chasisnumber);
            if (record == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ValidatePolicyNumber(int policynumber)
        {
            var record=await serverContext.Policies.FirstOrDefaultAsync(p=>p.PolicyNumber == policynumber);
            if (record == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Guid> GetUserId(string Username)
        {
            var userRecord=await dataContext.portaluser.FirstOrDefaultAsync(p=>p.UserName == Username);
            return userRecord == null ? Guid.Empty : userRecord.Id;
        }
    }
}
