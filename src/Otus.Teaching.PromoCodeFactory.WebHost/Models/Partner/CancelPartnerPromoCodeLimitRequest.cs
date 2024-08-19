using System;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models
{
    public class CancelPartnerPromoCodeLimitRequest
    {
        public DateTime CancelDate { get; set; }
    }
}