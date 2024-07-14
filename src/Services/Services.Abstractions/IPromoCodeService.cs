using Services.Contracts.Customer;
using Services.Contracts.Emploee;
using Services.Contracts.PromoCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IPromoCodeService
    {
        /// <summary>
        /// Получить список клиентов.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО клиента. </returns>
        Task<IEnumerable<PromoCodeShortResponseDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить сотрудника.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО сотрудника. </returns>
        Task<PromoCodeResponceDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создать курс.
        /// </summary>
        /// <param name="GivePromoCodeRequestDto"> ДТО создаваемого курса. </param>
        /// <returns> Идентификатор. </returns>
        Task<PromoCodeResponceDto> CreatePromoCodeAsync(GivePromoCodeRequestDto givePromoCodeRequestDto, CancellationToken cancellationToken);
    }
}
