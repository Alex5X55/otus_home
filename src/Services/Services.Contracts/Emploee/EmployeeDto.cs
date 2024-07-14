using Services.Contracts.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.Emploee
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public string FullName { get; set; } //=> $"{FirstName} {LastName}";

        public string Email { get; set; }

        public RoleDto Role { get; set; }

        public int AppliedPromocodesCount { get; set; }
    }
}
