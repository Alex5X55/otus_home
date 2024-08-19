using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.Partner
{
    public class PartnerPromoCodeLimitDto
    {
        public Guid Id { get; set; }

        public Guid PartnerId { get; set; }

        public virtual PartnerDto Partner { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? CancelDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Limit { get; set; }
    }
}
