using Services.Contracts.Partner;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeTests
{
    public class PartnerPromoCodeLimitResponseDtoBuilder
    {

        private Guid _id;
        private Guid _partnerId;
        private DateTime _createDate;
        private DateTime _cancelDate;
        private DateTime _endDate;
        private int _limit;


        public PartnerPromoCodeLimitResponseDtoBuilder()
        {
        }

        public PartnerPromoCodeLimitResponseDtoBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public PartnerPromoCodeLimitResponseDtoBuilder WithPartnerId(Guid partnerId)
        {
            _partnerId = partnerId;
            return this;
        }

        public PartnerPromoCodeLimitResponseDtoBuilder WithCreateDate(DateTime createDate)
        {
            _createDate = createDate;
            return this;
        }

        public PartnerPromoCodeLimitResponseDtoBuilder WithCancelDate(DateTime cancelDate)
        {
            _cancelDate = cancelDate;
            return this;
        }

        public PartnerPromoCodeLimitResponseDtoBuilder WithEndDate(DateTime endDate)
        {
            _endDate = endDate;
            return this;
        }

        public PartnerPromoCodeLimitResponseDtoBuilder WithLimit(int limit)
        {
            _limit = limit;
            return this;
        }


        public PartnerPromoCodeLimitResponseDto Build()
        {
            return new PartnerPromoCodeLimitResponseDto()
            {
                 Id =_id,
                 PartnerId = _partnerId,
                 CreateDate = _createDate,
                 CancelDate = _cancelDate,
                 EndDate = _endDate,
                 Limit = _limit
            };
        }

    }
}
