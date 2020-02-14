using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApiFundamental.Data
{
    public class CampContext:DbContext
    {
        public CampContext():base("CodeCampConnectionString")
        {   
        }


    }
}