using Services.Contracts.Customer;
using Services.Contracts.Emploee;
using Services.Contracts.Preference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.PromoCode
{
    public class PromoCodeDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }

        public string ServiceInfo { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public string PartnerName { get; set; }

        public virtual EmployeeDto? PartnerManager { get; set; }

        public virtual Guid? PartnerManagerId { get; set; }

        public virtual PreferenceDto Preference { get; set; }

        public virtual Guid? PreferenceId { get; set; }

        public virtual CustomerDto Customer { get; set; }

        public virtual Guid? CustomerId { get; set; }
    }
}
