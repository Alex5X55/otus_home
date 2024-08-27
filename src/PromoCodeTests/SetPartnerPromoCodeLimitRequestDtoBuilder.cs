using Services.Contracts.Partner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeTests
{
    public class SetPartnerPromoCodeLimitRequestDtoBuilder
    {
        private Guid _partnerId;
        private DateTime _endDate;
        private int _limit;

        public SetPartnerPromoCodeLimitRequestDtoBuilder()
        {
        }

        public SetPartnerPromoCodeLimitRequestDtoBuilder WithPartnerId(Guid partnerId)
        {
            _partnerId = partnerId;
            return this;
        }
        public SetPartnerPromoCodeLimitRequestDtoBuilder WithEndDate(DateTime dateEnd)
        {
            _endDate = dateEnd;
            return this;
        }
        public SetPartnerPromoCodeLimitRequestDtoBuilder WithLimit(int limit)
        {
            _limit = limit;
            return this;
        }


        public SetPartnerPromoCodeLimitRequestDto Build()
        {
            return new SetPartnerPromoCodeLimitRequestDto()
            {
                PartnerId = _partnerId,
                EndDate = _endDate,
                Limit = _limit
            };
        }

    }
}
