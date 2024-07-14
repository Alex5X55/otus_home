using Otus.Teaching.PromoCodeFactory.WebHost.Models.Preference;
using Services.Contracts.Preference;
using System;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.PromoCode
{
    public class PromoCodeResponce
    {
        public Guid Id { get; set; }

        public string? Code { get; set; }

        public string? ServiceInfo { get; set; }

        public string? BeginDate { get; set; }

        public string? EndDate { get; set; }

        public string PartnerName { get; set; }

        public PreferenceResponse Preference { get; set; }

        public Guid? PartnerManagerId { get; set; }
    }
}
