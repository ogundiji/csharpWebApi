using AutoMapper;
using WebApiFundamental.Core.Data.Entities;
using WebApiFundamental.Core.Model;
using WebApiFundamental.Core.Models;


namespace WebApiFundamental.Persistence
{
    public class CampMappingProfile:Profile
    {
        public CampMappingProfile()
        {
            //Any time that one of the member is not mapping then use formember
            CreateMap<Camp, CampDto>()
                .ForMember(x => x.Venue, opt => opt.MapFrom(x => x.Location.VenueName))
                .ReverseMap();

            CreateMap<Talk, TalkDto>()
                .ReverseMap()
                .ForMember(x=>x.Speaker, opt=>opt.Ignore())
                .ForMember(x=>x.Camp, opt=>opt.Ignore());

            CreateMap<Speaker, SpeakerDto>()
                .ReverseMap();

            CreateMap<ApplicationUser, ApplicationDTO>()
                .ReverseMap();
        }
    }
}