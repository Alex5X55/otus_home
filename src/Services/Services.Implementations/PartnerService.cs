using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Services.Abstractions;
using Services.Contracts.Partner;
using Services.Repositories.Abstractions;
using Services.Implementations.Exceptions.Partner;
using System.Collections.Generic;

namespace Services.Implementations
{
    public class PartnerService : IPartnerService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Partner> _partnerRepository;
        private readonly IRepository<PartnerPromoCodeLimit> _partnerPromoCodeLimitRepository;
        //private readonly IRepository<CustomerPromoCode> _customerPromoCodeRepository;

        public PartnerService(
                IMapper mapper,
                IRepository<Partner> partnerRepository,
                IRepository<PartnerPromoCodeLimit> partnerPromoCodeLimitRepository
          //      IRepository<CustomerPromoCode> customerPromoCodeRepository
                )
        // IBusControl busControl)
        {
            _mapper = mapper;
            _partnerRepository = partnerRepository;
            _partnerPromoCodeLimitRepository = partnerPromoCodeLimitRepository;
          //  _customerPromoCodeRepository = customerPromoCodeRepository;
            //_busControl = busControl;
        }


        /// <summary>
        /// Получить список партнеров.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО партнера. </returns>
        public async Task<IEnumerable<PartnerDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var partners = await _partnerRepository.GetAllAsync(cancellationToken, true);

            var partnersDtoList = partners.Select(x =>
                   _mapper.Map<Partner, PartnerDto>(x)
               ).ToList();

            return partnersDtoList;
        }

        /// <summary>
        /// Получить партнера по Id.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО партнера. </returns>
        public async Task<Partner> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var partner = await _partnerRepository.GetByIdAsync(id, cancellationToken);
            return partner;
        }



        /// <summary>
        /// Создать курс.
        /// </summary>
        /// <param name="setPartnerPromoCodeLimitRequestDto"> ДТО устанавливаемого лимита. </param>
        /// <returns> Идентификатор. </returns>
        public async Task<PartnerPromoCodeLimitResponseDto> SetPartnerPromoCodeLimitAsync(SetPartnerPromoCodeLimitRequestDto setPartnerPromoCodeLimitRequestDto, CancellationToken cancellationToken)
        {
            PartnerPromoCodeLimit currLimit = null;
            var partner = await _partnerRepository.GetByIdAsync(setPartnerPromoCodeLimitRequestDto.PartnerId, cancellationToken);
            if (partner == null)
                throw new PartnerNotFoundException();
            if (!partner.IsActive)
                throw new PartnerIsBlokedException();
            if (setPartnerPromoCodeLimitRequestDto.Limit == 0)
                throw new PartnerLimitIsEmptyException();

            if (partner.PartnerLimits.Count != 0)
            {
                var currCreateDate = partner.PartnerLimits.Max(cd => cd.CreateDate);

                //var currLimit = (from pl in partner.PartnerLimits
                //                 where pl.CreateDate == currCreateDate
                //                 select pl).FirstOrDefault();

                currLimit = partner.PartnerLimits.FirstOrDefault(pl => pl.CreateDate == currCreateDate);
            }


            if (currLimit is not null)
            {
              currLimit.CancelDate = DateTime.Now;
            }

            var newLimit = new PartnerPromoCodeLimit()
            {
                Id = Guid.NewGuid(),
                Limit = setPartnerPromoCodeLimitRequestDto.Limit,
                Partner = partner,
                PartnerId = partner.Id,
                CreateDate = DateTime.Now,
                CancelDate = DateTime.MaxValue,
                EndDate = setPartnerPromoCodeLimitRequestDto.EndDate,
            };

            await _partnerPromoCodeLimitRepository.AddAsync(newLimit, cancellationToken);

            partner.NumberIssuedPromoCodes = 0;
            await _partnerRepository.UpdateAsync(partner, cancellationToken);

            var partnerPromoCodeLimit = await _partnerPromoCodeLimitRepository.GetByIdAsync(newLimit.Id, cancellationToken);
            return _mapper.Map<PartnerPromoCodeLimitResponseDto>(partnerPromoCodeLimit);
        }

        /// <summary>
        /// Создать курс.
        /// </summary>
        /// <param name="cancelPartnerPromoCodeLimitRequestDto"> ДТО отменяемого лимита. </param>
        /// <returns> Идентификатор. </returns>
        public async Task<PartnerPromoCodeLimitResponseDto> CancelPartnerPromoCodeLimitAsync(CancelPartnerPromoCodeLimitRequestDto cancelPartnerPromoCodeLimitRequestDto, CancellationToken cancellationToken)
        {
            PartnerPromoCodeLimit currLimit = null;
            PartnerPromoCodeLimit partnerPromoCodeLimit = null;
            var partner = await _partnerRepository.GetByIdAsync(cancelPartnerPromoCodeLimitRequestDto.PartnerId, cancellationToken);
            if (partner == null)
                throw new PartnerNotFoundException();
            if (!partner.IsActive)
                throw new PartnerIsBlokedException();
            if (partner.PartnerLimits.Count == 0)
                throw new PartnerLimitIsNotSetException();

            var currCreateDate = partner.PartnerLimits.Max(cd => cd.CreateDate);

                //var currLimit = (from pl in partner.PartnerLimits
                //                 where pl.CreateDate == currCreateDate
                //                 select pl).FirstOrDefault();

            currLimit = partner.PartnerLimits.FirstOrDefault(pl => pl.CreateDate == currCreateDate);
            
            if ((currLimit is not null)&&(currLimit.EndDate > cancelPartnerPromoCodeLimitRequestDto.CancelDate))
            {
                currLimit.CancelDate = cancelPartnerPromoCodeLimitRequestDto.CancelDate;
                partnerPromoCodeLimit = await _partnerPromoCodeLimitRepository.UpdateAsync(currLimit, cancellationToken);
                return _mapper.Map<PartnerPromoCodeLimitResponseDto>(partnerPromoCodeLimit);
            }

            //var partnerPromoCodeLimit = await _partnerPromoCodeLimitRepository.GetByIdAsync(currLimit.Id, cancellationToken);
            
            return new PartnerPromoCodeLimitResponseDto();
        }
    }
}
