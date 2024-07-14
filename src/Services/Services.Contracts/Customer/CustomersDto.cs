using Services.Contracts.CustomerPreference;
using Services.Contracts.CustomerPromoCode;
using Services.Contracts.Preference;
using Services.Contracts.PromoCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.Customer
{
    public class CustomersDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        //public virtual List<CustomerPreferenceDto>? Preferences { get; set; }
        public List<CustomerPreferenceDto>? Preferences { get; set; }
        public List<CustomerPromoCodeDto>? PromoCodes { get; set; }
    }
}
