using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class Preference
         : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
