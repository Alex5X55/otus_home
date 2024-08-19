using System;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.PromoCode
{
    public class GivePromoCodeRequest
    {
        public string Code { get; set; }

        public string ServiceInfo { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid PartnerId { get; set; }

        public Guid? PreferenceId { get; set; }

        public Guid? PartnerManagerId { get; set; }
    }
}