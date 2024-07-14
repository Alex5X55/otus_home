using System;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.Emploee
{
    public class UpdateEmployeeRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid RoleId { get; set; }
    }
}
