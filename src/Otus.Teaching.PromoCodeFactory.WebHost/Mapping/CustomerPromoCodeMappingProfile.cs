using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.PromoCode;
using Services.Contracts.CustomerPreference;
using Services.Contracts.CustomerPromoCode;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Mapping
{
    public class CustomerPromoCodeMappingProfile : Profile
    {
        public CustomerPromoCodeMappingProfile()
        {
           CreateMap<CustomerPromoCode, CustomerPromoCodeDto>();
        }
    }
}
