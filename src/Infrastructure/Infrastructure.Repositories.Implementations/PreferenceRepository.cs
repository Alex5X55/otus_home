using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using NPoco;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Services.Contracts.Preference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations
{
    public class PreferenceRepository : EfRepository<Preference> 
    {
        public PreferenceRepository(DatabaseContext context) : base(context) { }

        /// <summary>
        /// Получить список предпочтений.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns> Список предпочтений. </returns>
        public override async Task<IEnumerable<Preference>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            var query = Context.Set<Preference>().AsQueryable();
            return await query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Получить сотрудника.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО курса. </returns>
        public override async Task<Preference> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = Context.Set<Preference>().AsQueryable();
            return await query.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
        }



    }
}
