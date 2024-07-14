using Services.Contracts.Emploee;
using Services.Contracts.Preference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IPreferenceService
    {
        /// <summary>
        /// Получить список предпочтений.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО предпочтений. </returns>
        Task<IEnumerable<PreferenceDto>> GetAllAsync(CancellationToken cancellationToken);

        Task<PreferenceDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    }
}
