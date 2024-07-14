using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Services.Abstractions;
using Services.Contracts.Emploee;
using Services.Contracts.Preference;
using Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class PreferenceService : IPreferenceService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Preference> _preferenceRepository;

        public PreferenceService(
                IMapper mapper,
                IRepository<Preference> preferenceRepository)
        // IBusControl busControl)
        {
            _mapper = mapper;
            _preferenceRepository = preferenceRepository;
            //_busControl = busControl;
        }



        /// <summary>
        /// Получить списоку сотрудников.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО курса. </returns>
        public async Task<IEnumerable<PreferenceDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var preferences = await _preferenceRepository.GetAllAsync(cancellationToken, true);
            var preferencesDtolList = preferences.Select(x =>
                   _mapper.Map<Preference, PreferenceDto>(x)
               ).ToList();

            return preferencesDtolList;
        }


        /// <summary>
        /// Получить сотрудника.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО курса. </returns>
        public async Task<PreferenceDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var preference = await _preferenceRepository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<Preference, PreferenceDto>(preference);
        }

    }
}
