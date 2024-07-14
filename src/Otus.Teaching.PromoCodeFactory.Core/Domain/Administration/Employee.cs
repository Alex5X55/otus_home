using System;
using System.Collections.Generic;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.Administration
{
    public class Employee
        : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }    
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public string FullName => $"{FirstName} {LastName}";

        public string Email { get; set; }

        //public List<Role> Roles { get; set; }
        public virtual Role Role { get; set; }

       /// <summary>
       public virtual Guid? RoleId { get; set; }
       /// </summary>

        public int AppliedPromocodesCount { get; set; }

        public bool Deleted { get; set; }

    }
}