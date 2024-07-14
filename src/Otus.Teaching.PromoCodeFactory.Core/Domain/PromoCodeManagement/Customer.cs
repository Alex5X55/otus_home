using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class Customer : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public virtual IList<CustomerPreference> Preferences { get; set; }
        public virtual IList<CustomerPromoCode> PromoCodes { get; set; }
        // public virtual PromoCode Promocode { get; set; }
        // public virtual Guid PromocodeId { get; set; }
    }
}
