using Services.Contracts.Customer;
using Services.Contracts.Preference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.CustomerPreference
{
    public class CustomerPreferenceDto
    {
        public Guid Id { get; set; }
        public CustomerDto Customer { get; set; }
        public Guid CustomerId { get; set; }
        public PreferenceDto Preference { get; set; }
        public Guid PreferenceId { get; set; }
    }
}
