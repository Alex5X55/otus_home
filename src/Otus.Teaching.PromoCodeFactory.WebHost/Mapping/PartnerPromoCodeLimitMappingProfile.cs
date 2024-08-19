using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Emploee;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.PromoCode;
using Services.Contracts.CustomerPromoCode;
using Services.Contracts.Emploee;
using Services.Contracts.Partner;
using Services.Contracts.PromoCode;
using StackExchange.Profiling.Internal;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Mapping
{
    public class PartnerPromoCodeLimitMappingProfile : Profile
    {
        public PartnerPromoCodeLimitMappingProfile()
        {
            CreateMap<PartnerPromoCodeLimit, PartnerPromoCodeLimitDto>().ReverseMap();

            CreateMap<PartnerPromoCodeLimitDto, PartnerPromoCodeLimitResponse>()
                .ForMember(d => d.CreateDate, s => s.MapFrom(s => s.CreateDate.ToString(CultureInfo.CurrentCulture)))
                .ForMember(d => d.CancelDate, s => s.MapFrom(s => s.CancelDate.Value.ToString(CultureInfo.CurrentCulture) ?? ""))
                .ForMember(d => d.EndDate, s => s.MapFrom(s => s.EndDate.ToString(CultureInfo.CurrentCulture)));

            CreateMap<SetPartnerPromoCodeLimitRequestDto, PartnerPromoCodeLimit>()
                .ForMember(d => d.Id, s => s.Ignore())
                .ForMember(d => d.Partner, s => s.Ignore())
                .ForMember(d => d.CreateDate, s => s.Ignore())
                .ForMember(d => d.CancelDate, s => s.Ignore());

            CreateMap<PartnerPromoCodeLimitResponseDto, PartnerPromoCodeLimit>()
                .ForMember(d => d.Partner, s => s.Ignore());

            CreateMap<PartnerPromoCodeLimit, PartnerPromoCodeLimitResponseDto>();
            CreateMap<PartnerPromoCodeLimitResponseDto, PartnerPromoCodeLimitResponse>()
                .ForMember(d => d.CreateDate, s => s.MapFrom(s => s.CreateDate.ToString(CultureInfo.CurrentCulture)))
                .ForMember(d => d.CancelDate, s => s.MapFrom(s => s.CancelDate.Value.ToString(CultureInfo.CurrentCulture) ?? ""))
                .ForMember(d => d.EndDate, s => s.MapFrom(s => s.EndDate.ToString(CultureInfo.CurrentCulture))); ;

            CreateMap<CancelPartnerPromoCodeLimitRequestDto, PartnerPromoCodeLimit>()
                .ForMember(d => d.Id, s => s.Ignore())
                .ForMember(d => d.Partner, s => s.Ignore())
                .ForMember(d => d.CreateDate, s => s.Ignore())
                .ForMember(d => d.EndDate, s => s.Ignore())
                .ForMember(d => d.Limit, s => s.Ignore());

            /*  CreateMap<PromoCode, PromoCodeDto>().ReverseMap();
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
                  .ForMember(d => d.CustomerId, s => s.Ignore())
                  .ForMember(d => d.Partner, s => s.Ignore());

                */
        }
    }
}
