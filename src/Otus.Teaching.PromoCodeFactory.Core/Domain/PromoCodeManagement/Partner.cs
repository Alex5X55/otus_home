using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class Partner : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberIssuedPromoCodes { get; set; }
        public bool IsActive { get; set; }
        public virtual IList<PartnerPromoCodeLimit> PartnerLimits { get; set; }
    }
}
