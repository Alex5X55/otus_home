using System;
using System.Collections.Generic;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Role;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.Emploee
{
    public class EmployeeModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

        public RoleModel Role { get; set; }

        public int AppliedPromocodesCount { get; set; }
    }
}