using AutoMapper;
using DigimonApp.Domain.Models;
using DigimonApp.Resources;

namespace DigimonApp.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveDigimonResource, Digimon>();
            CreateMap<SaveUserResource, User>();
        }
    }
}
