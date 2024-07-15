using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Emploee;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.PromoCode;
using Services.Contracts.CustomerPromoCode;
using Services.Contracts.Emploee;
using Services.Contracts.PromoCode;
using System;
using System.Globalization;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Mapping
{
    public class PromoCodeMappingProfile : Profile
    {
        public PromoCodeMappingProfile()
        {
            CreateMap<PromoCode, PromoCodeDto>().ReverseMap();
            CreateMap<PromoCodeDto, PromoCodeShortResponse>();
            CreateMap<PromoCodeShortResponseDto, PromoCodeShortResponse>().ReverseMap();
            CreateMap<PromoCode, PromoCodeShortResponseDto>();
            CreateMap<PromoCode, PromoCodeResponceDto>()
                .ForMember(d => d.BeginDate, s => s.MapFrom(s => s.BeginDate.ToString(CultureInfo.CurrentCulture)))
                .ForMember(d => d.EndDate, s => s.MapFrom(s => s.EndDate.ToString(CultureInfo.CurrentCulture)));
            CreateMap<GivePromoCodeRequest, GivePromoCodeRequestDto>()
                .ForMember(d => d.Id, s => s.MapFrom(s => Guid.NewGuid()));
            CreateMap<GivePromoCodeRequestDto, PromoCode>()
                .ForMember(d => d.PartnerManager, s => s.Ignore())
                .ForMember(d => d.Preference, s => s.Ignore())
                .ForMember(d => d.Customer, s => s.Ignore())
                .ForMember(d => d.CustomerId, s => s.Ignore());
        }
    }
}
