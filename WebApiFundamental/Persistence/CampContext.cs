using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using WebApiFundamental.Core.Data.Entities;

namespace WebApiFundamental.Persistence
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
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}