using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using Services.Contracts.Partner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeWebApiTest
{
    public class SetPartnerPromoCodeLimitRequestBuilder
    {
        private DateTime _endDate;
        private int _limit;

        public SetPartnerPromoCodeLimitRequestBuilder()
        {
        }

        public SetPartnerPromoCodeLimitRequestBuilder WithEndDate(DateTime dateEnd)
        {
            _endDate = dateEnd;
            return this;
        }
        public SetPartnerPromoCodeLimitRequestBuilder WithLimit(int limit)
        {
            _limit = limit;
            return this;
        }


        public SetPartnerPromoCodeLimitRequest Build()
        {
            return new SetPartnerPromoCodeLimitRequest()
            {
                EndDate = _endDate,
                Limit = _limit
            };
        }

    }
}
