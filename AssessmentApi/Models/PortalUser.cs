using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AssessmentApi.Models
{
    public class PortalUser
    {
        public PortalUser()
        {
            this.UserPolicies = new List<UserPolicyList>();

        }


        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        // Define the one-to-many relationship using annotations
        [InverseProperty("PortalUser")]

        [JsonIgnore]

        public List<UserPolicyList> UserPolicies { get; set; }
    }
}
