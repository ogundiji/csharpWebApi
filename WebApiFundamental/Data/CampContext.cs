using System.Data.Entity;

namespace WebApiFundamental.Data
{
    public class CampContext:DbContext
    {
        public CampContext():base("CodeCampConnectionString")
        {   

        }
    }
}