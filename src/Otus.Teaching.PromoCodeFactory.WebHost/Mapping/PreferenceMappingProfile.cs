using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Preference;
using Services.Contracts.Preference;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Mapping
{
    public class PreferenceMappingProfile : Profile
    {
        public PreferenceMappingProfile() 
        {
            CreateMap<PreferenceResponse, PreferenceDto>().ReverseMap();
            CreateMap<Preference, PreferenceDto>().ReverseMap();
        }
    }
}
