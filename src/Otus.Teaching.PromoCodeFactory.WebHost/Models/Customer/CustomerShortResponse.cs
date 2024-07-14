using System;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.Customer
{
    public class CustomerShortResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}