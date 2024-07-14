using Services.Contracts.Customer;
using Services.Contracts.PromoCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.CustomerPromoCode
{
    public class CustomerPromoCodeDto
    {
        public Guid Id { get; set; }
        public CustomerDto Customer { get; set; }
        public Guid CustomerId { get; set; }
        public PromoCodeDto PromoCode { get; set; }
        public Guid PromoCodeId { get; set; }
    }
}
