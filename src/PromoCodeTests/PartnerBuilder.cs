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
    public class PartnerBuilder
    {
        private Guid _Id;
        private string _Name;
        private int _NumberIssuedPromoCodes;
        private bool _IsActive;
        private IList<PartnerPromoCodeLimit> _PartnerLimits;

        public PartnerBuilder(){}

        public PartnerBuilder WithId(Guid Id) 
        { 
            _Id = Id; 
            return this; 
        }
        public PartnerBuilder WithName(string Name)
        { 
            _Name = Name; 
            return this; 
        }
        public PartnerBuilder WithNumberIssuedPromoCodes(int NumberIssuedPromoCodes) 
        {
            _NumberIssuedPromoCodes = NumberIssuedPromoCodes;
            return this;
        }
        public PartnerBuilder WithIsActive(bool IsActive)
        {
            _IsActive = IsActive; 
            return this; 
        }
        public PartnerBuilder WithPartnerLimits(IList<PartnerPromoCodeLimit> PartnerLimits) 
        {
            _PartnerLimits = PartnerLimits;
            return this;
        }

        public Partner Build()
        {
            return new Partner()
            {
              Id = _Id,
              Name = _Name,
              NumberIssuedPromoCodes = _NumberIssuedPromoCodes,
              IsActive = _IsActive,
              PartnerLimits = _PartnerLimits
            };
        }

    }
}
