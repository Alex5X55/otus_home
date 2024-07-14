using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Emploee;
using Services.Contracts.Emploee;
using System;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Mapping
{
    public class EmploeeMappingProfile : Profile
    {
        public EmploeeMappingProfile() 
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(d => d.Role, s => s.MapFrom(s => s.Role));
            //.ForMember(d => d.De, s => s.Ignore());

            CreateMap<CreatingEmployeeDto, Employee>()
                .ForMember(d => d.Id, s => s.MapFrom(s => Guid.NewGuid()))
                .ForMember(d => d.Role, s => s.Ignore())
                .ForMember(d => d.AppliedPromocodesCount, s => s.Ignore())
                .ForMember(d => d.Deleted, s => s.Ignore());
            //.ForMember(d => d.De, s => s.Ignore());



            CreateMap<EmployeeDto, EmployeeModel>()
            .ForMember(d => d.FullName, s => s.MapFrom(s => $"{s.FirstName} {s.LastName}"))
            .ForMember(d => d.Role, s => s.MapFrom(s => s.Role));

            //.ForMember(d => d.FirstName, s => s.Ignore())
            //.ForMember(d => d.LastName, s => s.Ignore()); //$"{s.FirstName} {s.LastName}");

            //.ForMember(d => d.AppliedPromocodesCount, s => s.Ignore());

            // .ForMember(d => d.Id, s => s.MapFrom(src => src.Id))
            // .ForMember(d => d.FirstName, s => s.Ignore())
            //  .ForMember(d => d.LastName, s => s.Ignore())
            // .ForMember(d => d.Email, s => s.Ignore())
            // .ForMember(d => d.Roles, s => s.Ignore())
            // .ForMember(d => d.AppliedPromocodesCount, s => s.Ignore());
            CreateMap<EmployeeDto, EmployeeShortResponse>()
                .ForMember(d => d.FullName, s => s.MapFrom(s => $"{s.FirstName} {s.LastName}"))
                .ForMember(d => d.Role, s => s.MapFrom(s => s.Role));
            //.ForMember(d => d.Roles, s => s.Ignore());

            CreateMap<UpdateEmployeeRequest, UpdateEmployeeDto>();

            CreateMap<UpdateEmployeeDto, Employee>()
                .ForMember(d => d.Role, s => s.Ignore())
                .ForMember(d => d.AppliedPromocodesCount, s => s.Ignore())
                .ForMember(d => d.Email, s => s.Ignore())
                .ForMember(d => d.Deleted, s => s.Ignore());
        }
    }
}     
