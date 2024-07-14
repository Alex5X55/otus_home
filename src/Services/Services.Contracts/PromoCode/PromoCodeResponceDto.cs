using Services.Contracts.Preference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.PromoCode
{
    public class PromoCodeResponceDto
    {
        public Guid Id { get; set; }

        public string? Code { get; set; }

        public string? ServiceInfo { get; set; }

        public string? BeginDate { get; set; }

        public string? EndDate { get; set; }

        public string PartnerName { get; set; }

        public PreferenceDto Preference { get; set; }
        
        public Guid PreferenceId { get; set; }

        public Guid? PartnerManagerId { get; set; }

    }
}
