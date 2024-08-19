using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class PromoCode
        : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string ServiceInfo { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual Partner Partner { get; set; }

        public virtual Guid? PartnerId { get; set; }

        public virtual Employee? PartnerManager { get; set; }

        public virtual Guid? PartnerManagerId { get; set; }
        
        public virtual Preference Preference { get; set; }

        public virtual Guid? PreferenceId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Guid? CustomerId { get; set; }

    }
}
