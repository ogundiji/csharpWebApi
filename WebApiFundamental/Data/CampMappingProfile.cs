using AutoMapper;
using WebApiFundamental.Data.Entities;
using WebApiFundamental.Models;

namespace WebApiFundamental.Data
{
    public class CampMappingProfile:Profile
    {
        public CampMappingProfile()
        {
            //Any time that one of the member is not mapping then use formember
            CreateMap<Camp, CampModel>()
                .ForMember(x => x.Venue, opt => opt.MapFrom(x => x.Location.VenueName));
            CreateMap<Talk, TalkModel>().
                ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title));
            CreateMap<Speaker, SpeakerModel>();

               

        }
    }
}