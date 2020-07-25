using System.ComponentModel.DataAnnotations;

namespace WebApiFundamental.Core.Models
{
    public class TalkDto
    {
        public int TalkId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [StringLength(4096,MinimumLength =100)]
        public string Abstract { get; set; }

        [Required]
        [Range(100,500)]
        public int Level { get; set; }
        public SpeakerDto Speaker { get; set; }
    }
}