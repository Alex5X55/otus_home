using Services.Contracts.Customer;
using Services.Contracts.Emploee;
using Services.Contracts.Partner;
using Services.Contracts.PromoCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IPartnerService
    {
        /// <summary>
        /// Получить список партнеров.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО партнера. </returns>
        Task<IEnumerable<PartnerDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Создать курс.
        /// </summary>
        /// <param name="setPartnerPromoCodeLimitRequestDto"> ДТО устанавливаемого лимита. </param>
        /// <returns> Идентификатор. </returns>
        Task<PartnerPromoCodeLimitResponseDto> SetPartnerPromoCodeLimitAsync(SetPartnerPromoCodeLimitRequestDto setPartnerPromoCodeLimitRequestDto, CancellationToken cancellationToken);

        /// <summary>
        /// Создать курс.
        /// </summary>
        /// <param name="cancelPartnerPromoCodeLimitRequestDto"> ДТО отменяемого лимита. </param>
        /// <returns> Идентификатор. </returns>
        Task<PartnerPromoCodeLimitResponseDto> CancelPartnerPromoCodeLimitAsync(CancelPartnerPromoCodeLimitRequestDto cancelPartnerPromoCodeLimitRequestDto, CancellationToken cancellationToken);
    }
}
