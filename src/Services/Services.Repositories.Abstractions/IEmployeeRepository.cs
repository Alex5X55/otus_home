using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Services.Contracts.Emploee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Abstractions
{
    public interface IEmployeeRepository : EfRepository<Employee, Guid> //IRepository<Employee, Guid>
    {
        Task<List<Employee>> GetPagedAsync(EmploeeFilterDto filterDto);
    }
}
