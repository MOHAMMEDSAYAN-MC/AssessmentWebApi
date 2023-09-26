using AssessmentApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AssessmentApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<PortalUser> portaluser { get; set; }
        public DbSet<UserPolicyList> userpolicylist { get; set; }
    }
}
