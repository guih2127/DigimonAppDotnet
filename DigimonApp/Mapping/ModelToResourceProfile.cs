using AutoMapper;
using DigimonApp.Domain.Models;
using DigimonApp.Resources;

namespace DigimonApp.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Digimon, DigimonResource>();
        }
    }
}
