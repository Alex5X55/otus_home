using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Emploee;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Role;
using Services.Contracts.Emploee;
using Services.Contracts.Role;
using System;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Mapping
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, RoleModel>();
            CreateMap<RoleDto, RoleShortResponse>();
            CreateMap<CreatingRoleDto, Role>()
                .ForMember(d => d.Id, s => s.MapFrom(s => Guid.NewGuid()))
                .ForMember(d => d.Deleted, s => s.Ignore());
            CreateMap<UpdateRoleDto, Role>()
                .ForMember(d => d.Deleted, s => s.Ignore());


            // CreateMap<Employee, EmployeeDto>()
            //  .ForMember(d => d.Role, s => s.MapFrom(s => s.Role));
            //.ForMember(d => d.De, s => s.Ignore());

            //   CreateMap<EmployeeDto, EmployeeModel>()
            //  .ForMember(d => d.FullName, s => s.MapFrom(s => $"{s.FirstName} {s.LastName}"))
            //  .ForMember(d => d.Role, s => s.MapFrom(s => s.Role));

            //.ForMember(d => d.FirstName, s => s.Ignore())
            //.ForMember(d => d.LastName, s => s.Ignore()); //$"{s.FirstName} {s.LastName}");

            //.ForMember(d => d.AppliedPromocodesCount, s => s.Ignore());

            // .ForMember(d => d.Id, s => s.MapFrom(src => src.Id))
            // .ForMember(d => d.FirstName, s => s.Ignore())
            //  .ForMember(d => d.LastName, s => s.Ignore())
            // .ForMember(d => d.Email, s => s.Ignore())
            // .ForMember(d => d.Roles, s => s.Ignore())
            // .ForMember(d => d.AppliedPromocodesCount, s => s.Ignore());
            //  CreateMap<EmployeeDto, EmployeeShortResponse>()
            //      .ForMember(d => d.FullName, s => s.MapFrom(s => $"{s.FirstName} {s.LastName}"))
            //      .ForMember(d => d.Role, s => s.MapFrom(s => s.Role));
            //.ForMember(d => d.Roles, s => s.Ignore());




        }
    }
}
