using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class CustomerPromoCode : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Guid CustomerId { get; set; }
        public virtual PromoCode PromoCode { get; set; }
        public virtual Guid PromoCodeId { get; set; }
    }
}
