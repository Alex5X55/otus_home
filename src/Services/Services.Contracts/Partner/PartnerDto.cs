using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.Partner
{
    public class PartnerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberIssuedPromoCodes { get; set; }
        public bool IsActive { get; set; }
        public virtual List<PartnerPromoCodeLimitDto> PartnerLimits { get; set; }

    }
}
