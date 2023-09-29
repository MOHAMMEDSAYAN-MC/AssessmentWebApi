using AssessmentApi.Models;

namespace AssessmentApi.Services.Interfaces
{
    public interface IUserInterface
    {
        public Task<Guid> GetUserId(string Username);
        public Task<bool> ValidateCredentials(PortalUser portalUser);

        public Task<bool> ValidatePolicyNumber(int policyNumber);

        public Task<bool> ValidateChasisNumber(string chasisnumber);

        public Task<bool> AddUserPolicyDetails(UserPolicyListDto userPolicyListDto);

        public Task<IEnumerable<int>?> GetPolicyNumbers(Guid userId);

        public Task<object?> GetInsuredDetails(int policynumber);

        public Task<bool> DeleteUserPolicy(UserPolicyListDto userPolicyListDto);



    }
}
