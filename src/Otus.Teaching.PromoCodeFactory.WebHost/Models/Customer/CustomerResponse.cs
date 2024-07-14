using System;
using System.Collections.Generic;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Preference;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.PromoCode;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.Customer
{
    public class CustomerResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        //TODO: Добавить список предпочтений
        public List<PreferenceResponse>? Preferences { get; set; }
        public List<PromoCodeShortResponse>? PromoCodes { get; set; }
    }
}