using AssessmentApi.Models;

namespace AssessmentApi.Services.Interfaces
{
    public interface IUserInterface
    {
        public Task<bool> validateCredentials(PortalUser portalUser);

        public Task<bool> validatePolicyNumber(int policyNumber);

        public Task<bool> validateChasisNumber(string chasisnumber);

        public Task<bool> addUserPolicyDetails(UserPolicyListDto userPolicyListDto);

        public Task<IEnumerable<int>> getPolicyNumbers();

        public Task<object> getInsuredDetails(int policynumber);

        public Task<bool> deleteUserPolicy(int policynumber);



    }
}
