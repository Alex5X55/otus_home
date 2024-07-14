using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Customer;
using Services.Contracts.Customer;
using Services.Contracts.CustomerPreference;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Mapping
{
    public class CustomerPreferenceMappingProfile : Profile
    {
        public CustomerPreferenceMappingProfile() 
        {
            CreateMap<CustomerPreference, CustomerPreferenceDto>().ReverseMap();
        }
    }
}
