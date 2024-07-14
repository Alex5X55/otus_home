using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Preference;
using AutoMapper;
using System.Linq;
using Services.Contracts.Preference;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PreferenceController
        : ControllerBase
    {
        private readonly IPreferenceService _preferenceService;
        private readonly IMapper _mapper;
        private readonly ILogger<PreferenceController> _logger;

        public PreferenceController(IPreferenceService service, ILogger<PreferenceController> logger, IMapper mapper)
        {
            _preferenceService = service;
            _logger = logger;
            _mapper = mapper;
        }


        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<PreferenceResponse>> GetPreferencesAsync(CancellationToken cancellationToken)
        {
            var preferenceDto = await _preferenceService.GetAllAsync(cancellationToken);

            var preferenceModelList = preferenceDto.Select(x =>
             _mapper.Map<PreferenceDto, PreferenceResponse>(x)).ToList();

            return preferenceModelList;
        }
    }
}
