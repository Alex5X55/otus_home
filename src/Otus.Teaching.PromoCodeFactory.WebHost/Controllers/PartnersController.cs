using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.PromoCode;
using Services.Abstractions;
using Services.Contracts.PromoCode;
using System.Linq;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Emploee;
using Services.Contracts.Emploee;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Customer;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Preference;
using Services.Contracts.Customer;
using Services.Contracts.Preference;
using Services.Implementations;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using Services.Contracts.Partner;
using Services.Implementations.Exceptions.Partner;
using Otus.Teaching.PromoCodeFactory.WebHost.PromoCodeStatusCodeResults;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Промокоды
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PartnersController
        : ControllerBase
    {
        private readonly IPartnerService _partnerService;
        //private readonly IPreferenceService _preferenceService;
        private readonly IMapper _mapper;
        private readonly ILogger<PartnersController> _logger;

        public PartnersController(IPartnerService partnerService,
                                  //  IPreferenceService preferenceService,
                                   IMapper mapper,
                                   ILogger<PartnersController> logger)
        {
            _partnerService = partnerService;
            //_preferenceService = preferenceService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Получить список партнеров
        /// </summary>
        /// <param name="СancellationToken">сancellationToken</param>
        /// <returns>Список клиентов</returns>
        /// <remarks>
        /// Simple request:
        ///
        /// "https://localhost:9001/api/v1/Partners"
        ///
        /// </remarks>
        /// <response code="200">Получили список клиентов</response>
        [HttpGet]
        public async Task<List<PartnerResponse>> GetPartnersAsync(CancellationToken cancellationToken)
        {
            var partnerResponse = await _partnerService.GetAllAsync(cancellationToken);

            var partnersList = partnerResponse.Select(x =>
             _mapper.Map<PartnerDto, PartnerResponse>(x)).ToList();

            return partnersList;
        }


        /// <summary>
        /// Установить лимит партнеру на выдачу промокодов
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="SetPartnerPromoCodeLimitRequest">setPartnerPromoCodeLimitRequest</param>
        /// <param name="CancellationToken">cancellationToken</param>
        /// <returns>Созданый промокод</returns>
        /// <remarks>
        /// Simple request:
        /// POST
        /// "https://localhost:9001/api/v1/Partners/{id}/SetLimits"
        /// SetPartnerPromoCodeLimitRequest
        ///{
        ///"endDate": "2031-08-10T20:19:28.870Z",
        ///"limit": 55
        ///}  
        /// </remarks>
        /// <response code="200">Получили клиента</response>
        /// <response code="404">Партнер не найден</response>
        [HttpPost("{id:guid}/SetLimits")]
        public async Task<IActionResult> SetPartnerPromoCodeLimitAsync(Guid id, SetPartnerPromoCodeLimitRequest setPartnerPromoCodeLimitRequest, CancellationToken cancellationToken)
        {
            try
            {
                var setPartnerPromoCodeLimitRequestDto = new SetPartnerPromoCodeLimitRequestDto
                {
                    PartnerId = id,
                    EndDate = setPartnerPromoCodeLimitRequest.EndDate,
                    Limit = setPartnerPromoCodeLimitRequest.Limit
                };
                var partnerPromoCodeLimitResponseDto = await _partnerService.SetPartnerPromoCodeLimitAsync(setPartnerPromoCodeLimitRequestDto, cancellationToken);
                var partnerPromoCodeLimitResponse = _mapper.Map<PartnerPromoCodeLimitResponse>(partnerPromoCodeLimitResponseDto);
                return Ok(partnerPromoCodeLimitResponse);
            }
            catch (PartnerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (PartnerIsBlokedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(PartnerLimitIsEmptyException ex)
            {
                return new PartnerLimitIsEmptyObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Отменить лимит партнеру на выдачу промокодов
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="CancelPartnerPromoCodeLimitRequest">cancelPartnerPromoCodeLimitRequest</param>
        /// <param name="CancellationToken">cancellationToken</param>
        /// <returns>Созданый промокод</returns>
        /// <remarks>
        /// Simple request:
        /// POST
        /// "https://localhost:9001/api/v1/Partners/{id}/CancelLimits"
        /// SetPartnerPromoCodeLimitRequest
        ///{
        ///"endDate": "2031-08-10T20:19:28.870Z",
        ///"limit": 55
        ///}  
        /// </remarks>
        /// <response code="200">Получили клиента</response>
        /// <response code="404">Партнер не найден</response>
        [HttpPut("{id:guid}/CancelLimits")]
        public async Task<IActionResult> CancelPartnerPromoCodeLimitAsync(Guid id, CancelPartnerPromoCodeLimitRequest cancelPartnerPromoCodeLimitRequest, CancellationToken cancellationToken)
        {
            try
            {
                var cancelPartnerPromoCodeLimitRequestDto = new CancelPartnerPromoCodeLimitRequestDto
                {
                    PartnerId = id,
                    CancelDate = cancelPartnerPromoCodeLimitRequest.CancelDate
                };
                var partnerPromoCodeLimitResponseDto = await _partnerService.CancelPartnerPromoCodeLimitAsync(cancelPartnerPromoCodeLimitRequestDto, cancellationToken);
                var partnerPromoCodeLimitResponse = _mapper.Map<PartnerPromoCodeLimitResponse>(partnerPromoCodeLimitResponseDto);
                return Ok(partnerPromoCodeLimitResponse);
            }
            catch (PartnerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (PartnerIsBlokedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (PartnerLimitIsEmptyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (PartnerLimitIsNotSetException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

