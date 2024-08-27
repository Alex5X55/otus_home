using Otus.Teaching.PromoCodeFactory.WebHost.Models;
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
    public class PartnerPromoCodeLimitResponseBuilder
    {

        private Guid _id;
        private Guid _partnerId;
        private string _createDate;
        private string _cancelDate;
        private string _endDate;
        private int _limit;


        public PartnerPromoCodeLimitResponseBuilder()
        {
        }

        public PartnerPromoCodeLimitResponseBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public PartnerPromoCodeLimitResponseBuilder WithPartnerId(Guid partnerId)
        {
            _partnerId = partnerId;
            return this;
        }

        public PartnerPromoCodeLimitResponseBuilder WithCreateDate(DateTime createDate)
        {
            _createDate = createDate.ToString(CultureInfo.CurrentCulture);
            return this;
        }

        public PartnerPromoCodeLimitResponseBuilder WithCancelDate(DateTime cancelDate)
        {
            _cancelDate = cancelDate.ToString(CultureInfo.CurrentCulture);
            return this;
        }

        public PartnerPromoCodeLimitResponseBuilder WithEndDate(DateTime endDate)
        {
            _endDate = endDate.ToString(CultureInfo.CurrentCulture);
            return this;
        }

        public PartnerPromoCodeLimitResponseBuilder WithLimit(int limit)
        {
            _limit = limit;
            return this;
        }


        public PartnerPromoCodeLimitResponse Build()
        {
            return new PartnerPromoCodeLimitResponse()
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
