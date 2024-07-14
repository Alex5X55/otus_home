using Services.Contracts.Role;
using System.Collections.Generic;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.Emploee
{
    public class CreatingEmploeeModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Email { get; set; }

        public List<RoleDto> Roles { get; set; }

        public int AppliedPromocodesCount { get; set; }
    }
}
