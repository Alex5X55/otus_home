using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Customer;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Emploee;
using Services.Contracts.Customer;
using Services.Contracts.Emploee;
using System;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Mapping
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile() 
        {
            CreateMap<CustomerDto, CustomerShortResponse>()
            .ForMember(d => d.FullName, s => s.MapFrom(s => $"{s.FirstName} {s.LastName}"));
            CreateMap<Customer, CustomerDto>();
            CreateMap<CreatingCustomerDto, Customer>()
              .ForMember(d => d.Id, s => s.MapFrom(s => Guid.NewGuid()))
              .ForMember(d => d.PromoCodes, s => s.Ignore())
              .ForMember(d => d.Preferences, s => s.Ignore());
            CreateMap<UpdateCustomerDto, Customer>()
              .ForMember(d => d.PromoCodes, s => s.Ignore())
              .ForMember(d => d.Preferences, s => s.Ignore());
            //CreateMap<Customer, CustomersDto>();
            //CreateMap<CustomersDto, CustomerResponse>()
            //  .ForMember(d => d.FullName, s => s.MapFrom(s => $"{s.FirstName} {s.LastName}"))
            //  .ForMember(d => d.Preferences, s => s.MapFrom(s => s.Preferences));


        }
    }
}
