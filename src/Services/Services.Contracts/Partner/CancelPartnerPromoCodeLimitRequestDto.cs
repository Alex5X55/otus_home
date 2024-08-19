using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.Partner
{
    public class CancelPartnerPromoCodeLimitRequestDto
    {
        public Guid PartnerId { get; set; }
        public DateTime CancelDate { get; set; }
    }
}
