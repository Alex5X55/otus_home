using Otus.Teaching.PromoCodeFactory.WebHost.Models.Role;
using System;
using System.Collections.Generic;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.Emploee
{
    public class EmployeeShortResponse
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public RoleShortResponse Role { get; set; }
        //public List<RoleModel> Roles { get; set; }
    }
}