using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiFundamental.Core.Data.Entities
{
    public class RefreshToken
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
        [Required]
        [MaxLength(100)]
        public string ClientId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime IssuedUTC { get; internal set; }
        public DateTime ExpiresUtc { get; set; }

        [Required]
        public string ProtectedTicket { get; set; }
    }
}