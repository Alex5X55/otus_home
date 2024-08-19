using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.PromoCode
{
    public class GivePromoCodeRequestDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }

        public string ServiceInfo { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid PartnerId { get; set; }

        public Guid? PartnerManagerId { get; set; }

        public Guid? PreferenceId { get; set; }
    }
}
