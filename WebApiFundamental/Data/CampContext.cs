using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using WebApiFundamental.Data.Entities;

namespace WebApiFundamental.Data
{
    public class CampContext:IdentityDbContext<ApplicationUser>
    {
        public CampContext():base("CodeCampConnectionString")
        {   

        }

        public static CampContext Create()
        {
            return new CampContext();
        }

        public DbSet<Camp> camps { get; set; }
        public DbSet<Talk> talks { get; set; }
        public DbSet<Speaker> speakers { get; set; }
        public DbSet<Location> locations { get; set; }
    }
}