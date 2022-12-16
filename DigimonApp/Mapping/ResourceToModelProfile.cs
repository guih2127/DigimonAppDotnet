using AutoMapper;
using DigimonApp.Domain.Models;
using DigimonApp.Resources;

namespace DigimonApp.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveDigimonResource, Digimon>()
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => (DigimonLevelEnum)src.Level));

            CreateMap<SaveUserResource, User>();
        }
    }
}
