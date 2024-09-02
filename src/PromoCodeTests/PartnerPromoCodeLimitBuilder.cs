using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Services.Contracts.Partner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeTests
{
    public class PartnerPromoCodeLimitBuilder
    {
        private Guid _Id;
        private Guid _PartnerId;
        private Partner _Partner;
        private DateTime _CreateDate;
        private DateTime? _CancelDate;
        private DateTime _EndDate;
        private int _Limit;

        public PartnerPromoCodeLimitBuilder(){}

        public PartnerPromoCodeLimitBuilder WithId(Guid Id) 
        { 
            _Id = Id; 
            return this; 
        }
        public PartnerPromoCodeLimitBuilder WithPartnerId(Guid PartnerId)
        {
            _PartnerId = PartnerId; 
            return this; 
        }
        public PartnerPromoCodeLimitBuilder WithPartner(Partner Partner) 
        {
            _Partner = Partner;
            return this;
        }
        public PartnerPromoCodeLimitBuilder WithCreateDate(DateTime CreateDate)
        {
            _CreateDate = CreateDate; 
            return this; 
        }
        public PartnerPromoCodeLimitBuilder WithCancelDate(DateTime CancelDate) 
        {
            _CancelDate = CancelDate;
            return this;
        }

        public PartnerPromoCodeLimitBuilder WithEndDate(DateTime EndDate)
        {
            _EndDate = EndDate;
            return this;
        }

        public PartnerPromoCodeLimitBuilder WithLimit(int Limit)
        {
            _Limit = Limit;
            return this;
        }

        public PartnerPromoCodeLimit Build()
        {
            return new PartnerPromoCodeLimit()
            {
              Id = _Id,
              PartnerId = _PartnerId,
              Partner = _Partner,
              CreateDate = _CreateDate,
              CancelDate = _CancelDate,
              EndDate = _EndDate,
              Limit = _Limit
            };
        }

    }
}
