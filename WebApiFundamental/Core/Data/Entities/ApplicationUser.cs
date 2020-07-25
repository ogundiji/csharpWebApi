using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace WebApiFundamental.Core.Data.Entities
{
    public class ApplicationUser:IdentityUser
    {
       
            [Required]
            [MaxLength(100)]
            public string FirstName { get; set; }

            [Required]
            [MaxLength(100)]
            public string LastName { get; set; }

        
    }
}