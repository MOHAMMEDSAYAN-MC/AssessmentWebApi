using AssessmentApi.Data;
using AssessmentApi.Model;
using AssessmentApi.Models;
using AssessmentApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

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

        public async Task<bool> addUserPolicyDetails(UserPolicyListDto userPolicyListDto)
        {
            var res = await dataContext.portaluser.FirstOrDefaultAsync(p => p.UserName == userPolicyListDto.UserName);
            
            var result = new UserPolicyList
            {
                Id = Guid.NewGuid(),
                UserId = res.Id,
                PolicyNumber = userPolicyListDto.PolicyNumber

            };
            await dataContext.userpolicylist.AddAsync(result);
            await dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteUserPolicy(int policynumber)
        {
            var record= await dataContext.userpolicylist.FirstOrDefaultAsync(u=>u.PolicyNumber == policynumber);
            dataContext.userpolicylist.Remove(record);
            await dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<object> getInsuredDetails(int policynumber)
        {
           

            var policyDetails = await serverContext.Policies
                .Where(pol => pol.PolicyNumber == policynumber)
                .Join(
                    serverContext.Policyinsureds,
                    pol => pol.PolicyId,
                    polins => polins.PolicyId,
                    (pol, polins) => new { Policy = pol, PolicyInsured = polins })
                    .FirstOrDefaultAsync();


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

        public async Task<IEnumerable<int>> getPolicyNumbers()
        {
            var result = await dataContext.userpolicylist
                .Select(up => up.PolicyNumber)
                .ToListAsync();
            if (result.Any())
            {
                return result;
            }
            return null;
        }

        public async Task<bool> validateCredentials(PortalUser portalUser)
        {
            var rec = await dataContext.portaluser.FirstOrDefaultAsync(a => a.UserName == portalUser.UserName && a.Password == portalUser.Password);
            if (rec == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> validateChasisNumber(string chasisnumber)
        {
            var record = await serverContext.Vehicles.FirstOrDefaultAsync(v => v.ChasisNumber == chasisnumber);
            if (record == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> validatePolicyNumber(int policynumber)
        {
            var record=await serverContext.Policies.FirstOrDefaultAsync(p=>p.PolicyNumber == policynumber);
            if (record == null)
            {
                return false;
            }
            return true;
        }
    }
}
