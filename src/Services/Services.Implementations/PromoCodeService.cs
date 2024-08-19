using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Services.Abstractions;
using Services.Contracts.Customer;
using Services.Contracts.Preference;
using Services.Contracts.PromoCode;
using Services.Implementations.Exceptions.Partner;
using Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class PromoCodeService : IPromoCodeService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<PromoCode> _promocodeRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerPromoCode> _customerPromoCodeRepository;
        private readonly IRepository<Partner> _partnerRepository;

        public PromoCodeService(
                IMapper mapper,
                IRepository<PromoCode> promocodeRepository,
                IRepository<Customer> customerRepository,
                IRepository<CustomerPromoCode> customerPromoCodeRepository,
                IRepository<Partner> partnerRepository
                )
        // IBusControl busControl)
        {
            _mapper = mapper;
            _promocodeRepository = promocodeRepository;
            _customerRepository = customerRepository;
            _customerPromoCodeRepository = customerPromoCodeRepository;
            _partnerRepository = partnerRepository;
            //_busControl = busControl;
        }

        /// <summary>
        /// Получить список клиентов.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО клиента. </returns>
        public async Task<IEnumerable<PromoCodeShortResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var promoCodes = await _promocodeRepository.GetAllAsync(cancellationToken, true);
            var promoCodeShortResponseDtoList = promoCodes.Select(x =>
                   _mapper.Map<PromoCode, PromoCodeShortResponseDto>(x)
               ).ToList();

            return promoCodeShortResponseDtoList;

        }

        public async Task<PromoCodeResponceDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var promoCode = await _promocodeRepository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<PromoCode, PromoCodeResponceDto>(promoCode);
        }

        /// <summary>
        /// Создать курс.
        /// </summary>
        /// <param name="creatingEmplooeDto"> ДТО создаваемого курса. </param>
        /// <returns> Идентификатор. </returns>
        public async Task<PromoCodeResponceDto> CreatePromoCodeAsync(GivePromoCodeRequestDto givePromoCodeRequestDto, CancellationToken cancellationToken)
        {
            bool addPromoCodeFlag = false;
            PromoCode promoCode = new PromoCode();

            var customersAll = await _customerRepository.GetAllAsync(cancellationToken, false);
            var promoCodeResponceDto = new PromoCodeResponceDto()
            {
                Id = Guid.Empty,
                Code = "Промокоды раздавать некому"
            };

            var partner = await _partnerRepository.GetByIdAsync(givePromoCodeRequestDto.PartnerId, cancellationToken);
            if(partner == null)
                throw new PartnerNotFoundException();

            PartnerPromoCodeLimit currLimit = null;
            if (partner.PartnerLimits.Count != 0)
            {
                var currCreateDate = partner.PartnerLimits.Max(cd => cd.CreateDate);
                currLimit = partner.PartnerLimits.FirstOrDefault(pl => pl.CreateDate == currCreateDate);
            }


            if (customersAll == null) return promoCodeResponceDto;
            var customers = new List<Customer>(customersAll);

            foreach (var customer in customersAll)
            {
                if (currLimit.Limit == partner.NumberIssuedPromoCodes)
                    throw new PartnerLimitWasEndedException();
                
                if(customer.Preferences.Where(x => x.PreferenceId == givePromoCodeRequestDto.PreferenceId).ToList().Count > 0)
                {
                    if (!addPromoCodeFlag)
                    {
                        addPromoCodeFlag = true;
                        promoCode = _mapper.Map<PromoCode>(givePromoCodeRequestDto);
                        var pC = await _promocodeRepository.AddAsync(promoCode, cancellationToken);
                    }

                    var customerPromoCode = new CustomerPromoCode()
                    {
                        Id = Guid.NewGuid(),
                        CustomerId = customer.Id,
                        PromoCodeId = givePromoCodeRequestDto.Id
                    }; 
                    var customerPC = await _customerPromoCodeRepository.AddAsync(customerPromoCode, cancellationToken);
                    partner.NumberIssuedPromoCodes++;
                }
            }

            await _partnerRepository.UpdateAsync(partner, cancellationToken);

            return _mapper.Map<PromoCode, PromoCodeResponceDto>(promoCode);
        }
    }
}
