using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Emploee;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.PromoCode;
using Services.Contracts.Partner;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Mapping
{
    public class PartnerMappingProfile : Profile
    {
        public PartnerMappingProfile()
        {
            CreateMap<Partner, PartnerDto>().ReverseMap();
            CreateMap<PartnerDto, PartnerResponse>().ReverseMap();
        }
    }
}
