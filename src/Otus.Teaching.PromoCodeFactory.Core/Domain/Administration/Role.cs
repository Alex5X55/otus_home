using System;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.Administration
{
    public class Role
       : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public bool Deleted { get; set; }
    }
}