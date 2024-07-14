using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations
{
    public class PromoCodeRepository : EfRepository<PromoCode>
    {
        public PromoCodeRepository(DatabaseContext context) : base(context) { }

        /// <summary>
        /// Получить список предпочтений.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns> Список предпочтений. </returns>
        public override async Task<IEnumerable<PromoCode>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            var query = Context.Set<PromoCode>().AsQueryable();
            return await query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Получить список предпочтений.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns> Список предпочтений. </returns>
        public override async Task<PromoCode> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = Context.Set<PromoCode>().AsQueryable();
            return await query.SingleOrDefaultAsync(c => c.Id == id ,cancellationToken);
        }


        public override async Task<PromoCode> AddAsync(PromoCode promoCode, CancellationToken cancellationToken)
        {
            Context.PromoCodes.Add(promoCode);
            await Context.SaveChangesAsync(cancellationToken);

            return await GetByIdAsync(promoCode.Id, cancellationToken);
        }


    }
}
