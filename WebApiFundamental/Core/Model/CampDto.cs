using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace WebApiFundamental.Core.Models
{
    public class CampDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Moniker { get; set; }
        [Required]
        public DateTime EventDate { get; set; } = DateTime.MinValue;

        [Required]
        [Range(1,30)]
        public int Length { get; set; } = 1;

        public ICollection<TalkDto>Talks { get; set; }
        //Include Location as part of the model because it is one to one
        public string Venue { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationAddress3 { get; set; }
        public string LocationCityTown { get; set; }
        public string LocationStateProvince { get; set; }
        public string LocationPostalCode { get; set; }
        public string LocationCountry { get; set; }

    }
}