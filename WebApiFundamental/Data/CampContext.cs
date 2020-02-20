using System.Data.Entity;
using WebApiFundamental.Data.Entities;

namespace WebApiFundamental.Data
{
    public class CampContext:DbContext
    {
        public CampContext():base("CodeCampConnectionString")
        {   

        }

        public DbSet<Camp> camps { get; set; }
        public DbSet<Talk> talks { get; set; }
        public DbSet<Speaker> speakers { get; set; }
        public DbSet<Location> locations { get; set; }
    }
}