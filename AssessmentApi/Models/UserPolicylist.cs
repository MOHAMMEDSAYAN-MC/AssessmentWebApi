using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AssessmentApi.Models
{
    public class UserPolicyList
    {

        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public int PolicyNumber { get; set; }

        [ForeignKey("UserId")]

        [JsonIgnore]
        public PortalUser Portaluser { get; set; }
    }
}
