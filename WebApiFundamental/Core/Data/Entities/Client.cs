using System.ComponentModel.DataAnnotations;

namespace WebApiFundamental.Core.Data.Entities
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string ClientId { get; set; }

        [Required]
        public string Secret { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public bool Active { get; set; }

        public int RefreshTokenLifeTime { get; set; }
        public string AllowedOrigin { get; set; }
    }
}