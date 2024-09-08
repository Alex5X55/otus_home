using AutoFixture;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using Services.Contracts.Partner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeWebApiTest
{
    public static class ControllerTestData
    {
        static IFixture autoFixture = new Fixture();

        public static SetPartnerPromoCodeLimitRequest InitSetPartnerPromoCodeLimitRequest() 
        {
            return autoFixture.Build<SetPartnerPromoCodeLimitRequest>()
                .With(p => p.EndDate, DateTime.Now.AddMonths(1))
                .Create();
        }

        public static SetPartnerPromoCodeLimitRequestDto InitSetPartnerPromoCodeLimitRequestDto()
        {
            return autoFixture.Build<SetPartnerPromoCodeLimitRequestDto>()
                .With(p => p.PartnerId, Guid.NewGuid())
                .With(p => p.EndDate, DateTime.Now.AddDays(5))
                .Create();
        }


    }
}
