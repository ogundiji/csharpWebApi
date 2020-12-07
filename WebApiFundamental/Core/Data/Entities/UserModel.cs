using System.ComponentModel.DataAnnotations;

namespace WebApiFundamental.Core.Data.Entities
{
    public class UserModel
    {
          public string Firstname { get; set; }

          public string Lastname { get; set; }

            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            public bool active { get; set; }
        
    }
}