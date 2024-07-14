using System;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.PromoCode
{
    public class PromoCodeShortResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
    }
}