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

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Промокоды
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromocodesController
        : ControllerBase
    {
        private readonly IPromoCodeService _promocodeService;
        private readonly IPreferenceService _preferenceService;
        private readonly IMapper _mapper;
        private readonly ILogger<PromocodesController> _logger;

        public PromocodesController(IPromoCodeService promocodeService,
                                    IPreferenceService preferenceService,
                                   IMapper mapper,
                                   ILogger<PromocodesController> logger)
        {
            _promocodeService = promocodeService;
            _preferenceService = preferenceService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Получить список промокодов
        /// </summary>
        /// <param name="СancellationToken">сancellationToken</param>
        /// <returns>Список промокодов</returns>
        /// <remarks>
        /// Simple request:
        ///
        /// "https://localhost:9001/api/v1/Promocodes"
        ///
        /// </remarks>
        /// <response code="200">Получили список промокодов</response>
        [HttpGet]
        public async Task<ActionResult<List<PromoCodeShortResponse>>> GetPromocodesAsync(CancellationToken cancellationToken)
        {
            var promoCodeShortResponseDto = await _promocodeService.GetAllAsync(cancellationToken);

            var promoCodeList = promoCodeShortResponseDto.Select(x =>
             _mapper.Map<PromoCodeShortResponseDto, PromoCodeShortResponse>(x)).ToList();

            return promoCodeList;
        }

        /// <summary>
        /// Получить промокод по идентификатору id
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="CancellationToken">cancellationToken</param>
        /// <returns>Промокод</returns>
        /// <remarks>
        /// Simple request:
        ///
        /// "https://localhost:9001/api/v1/PromoCodes/id"
        ///
        /// </remarks>
        /// <response code="200">Получили промокод</response>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PromoCodeResponce>> GetPromocodeByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var promoCodeResponceDto = await _promocodeService.GetByIdAsync(id, cancellationToken);
            var promoCodeResponce = new PromoCodeResponce()
            {
                Id = promoCodeResponceDto.Id,
                Code = promoCodeResponceDto.Code,
                ServiceInfo = promoCodeResponceDto.ServiceInfo,
                BeginDate = promoCodeResponceDto.BeginDate,
                EndDate = promoCodeResponceDto.EndDate,
                PartnerName = promoCodeResponceDto.PartnerName,
                PartnerManagerId = promoCodeResponceDto.PartnerManagerId
            };

            var preferenceDto = await _preferenceService.GetByIdAsync(promoCodeResponceDto.PreferenceId, cancellationToken);
            promoCodeResponce.Preference = _mapper.Map<PreferenceDto, PreferenceResponse>(preferenceDto);

            return Ok(promoCodeResponce);
        }


        /// <summary>
        /// Создать новый промокод
        /// </summary>
        /// <param name="GivePromoCodeRequest">GivePromoCodeRequest</param>
        /// <param name="CancellationToken">cancellationToken</param>
        /// <returns>Созданый промокод</returns>
        /// <remarks>
        /// Simple request:
        /// POST
        /// "https://localhost:9001/api/v1/PromoCodes"
        /// GivePromoCodeRequest
        /// {
        ///  "code": "string",
        ///  "serviceInfo": "string",
        ///  "beginDate": "2024-07-15T01:46:57.847Z",
        ///  "endDate": "2024-07-15T01:46:57.847Z",
        ///  "partnerName": "string",
        ///  "preferenceId": "string",
        ///  "partnerManagerId": "string"
        /// }
        /// </remarks>
        /// <response code="200">Получили клиента</response>
        [HttpPost]
        public async Task<IActionResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request, CancellationToken cancellationToken)
        {
            var promoCodeResponceDto = await _promocodeService.CreatePromoCodeAsync(_mapper.Map<GivePromoCodeRequestDto>(request), cancellationToken);
            var promoCodeResponce = new PromoCodeResponce()
            {
                Id = promoCodeResponceDto.Id,
                Code = promoCodeResponceDto.Code,
                ServiceInfo = promoCodeResponceDto.ServiceInfo,
                BeginDate = promoCodeResponceDto.BeginDate,
                EndDate = promoCodeResponceDto.EndDate,
                PartnerName = promoCodeResponceDto.PartnerName,
                PartnerManagerId = promoCodeResponceDto.PartnerManagerId
            };

            var preferenceDto = await _preferenceService.GetByIdAsync(promoCodeResponceDto.PreferenceId, cancellationToken);
            promoCodeResponce.Preference = _mapper.Map<PreferenceDto, PreferenceResponse>(preferenceDto);

            return Ok(promoCodeResponce);
        }
    }
}