using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Services.Contracts.Emploee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services.Implementations.Mapping
{
    public class EmploeeMappingsProfile : Profile
    {
        public EmploeeMappingsProfile()
        {
           // CreateMap<EmploeeDto, Employee>()
             //   .ForMember(d => d.Deleted, s => s.Ignore());
           // CreateMap<CreatingEmploeeDto, Employee>()
           //     .ForMember(d => d.Id, s => s.Ignore());
        }
     

    }
}
