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
    public class PartnerRepository : EfRepository<Partner>
    {
        public PartnerRepository(DatabaseContext context) : base(context) { }

        public override async Task<IEnumerable<Partner>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            var query = Context.Set<Partner>().AsQueryable();
            return await query.ToListAsync(cancellationToken);
        }

    

        public override async Task<Partner> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
           var query = Context.Set<Partner>().AsQueryable();
           return await query.SingleOrDefaultAsync(c => c.Id == id ,cancellationToken);
        }


        public override async Task<Partner> UpdateAsync(Partner partner, CancellationToken cancellationToken)
        {
            Context.Partners.Update(partner);
            await Context.SaveChangesAsync(cancellationToken);
            return await GetByIdAsync(partner.Id, cancellationToken);
        }



        //   public override async Task<Partner> AddAsync(PromoCode promoCode, CancellationToken cancellationToken)
        //   {
        //       Context.PromoCodes.Add(promoCode);
        //       await Context.SaveChangesAsync(cancellationToken);

        //       return await GetByIdAsync(promoCode.Id, cancellationToken);
        //   }
    }
}
