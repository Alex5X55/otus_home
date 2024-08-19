using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.Partner
{
    public class SetPartnerPromoCodeLimitRequestDto
    {
        public Guid PartnerId { get; set; }
        public DateTime EndDate { get; set; }
        public int Limit { get; set; }
    }
}
