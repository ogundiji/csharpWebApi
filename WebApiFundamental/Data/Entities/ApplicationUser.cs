﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiFundamental.Data.Entities
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