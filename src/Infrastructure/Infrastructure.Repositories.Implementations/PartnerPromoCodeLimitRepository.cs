using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Infrastructure.Repositories.Implementations
{
    public class PartnerPromoCodeLimitRepository : EfRepository<PartnerPromoCodeLimit>
    {
        public PartnerPromoCodeLimitRepository(DatabaseContext context) : base(context) { }

        public override async Task<PartnerPromoCodeLimit> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = Context.Set<PartnerPromoCodeLimit>().AsQueryable();
            return await query.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public override async Task<PartnerPromoCodeLimit> AddAsync(PartnerPromoCodeLimit partnerPromoCodeLimit, CancellationToken cancellationToken)
        {
            Context.PartnerPromoCodeLimits.Add(partnerPromoCodeLimit);
            await Context.SaveChangesAsync(cancellationToken);

            return await GetByIdAsync(partnerPromoCodeLimit.Id, cancellationToken);
        }

        public override async Task<PartnerPromoCodeLimit> UpdateAsync(PartnerPromoCodeLimit partnerPromoCodeLimit, CancellationToken cancellationToken)
        {
            var query = Context.Set<PartnerPromoCodeLimit>().AsQueryable();
            var oldPartnerPromoCodeLimit = await query.SingleOrDefaultAsync(c => c.Id == partnerPromoCodeLimit.Id, cancellationToken);
            if (oldPartnerPromoCodeLimit != null)
                {
                 if (oldPartnerPromoCodeLimit.EndDate != partnerPromoCodeLimit.EndDate)
                    oldPartnerPromoCodeLimit.EndDate = partnerPromoCodeLimit.EndDate;
                 if (oldPartnerPromoCodeLimit.Limit != partnerPromoCodeLimit.Limit)
                    oldPartnerPromoCodeLimit.Limit = partnerPromoCodeLimit.Limit;

                 Context.PartnerPromoCodeLimits.Update(oldPartnerPromoCodeLimit);
                 await Context.SaveChangesAsync(cancellationToken);
                 return await GetByIdAsync(partnerPromoCodeLimit.Id, cancellationToken);
                }
          return null;
        }


        //  public override async Task<PartnerPromoCodeLimit> UpdateCancelAsync(PartnerPromoCodeLimit partnerPromoCodeLimit, CancellationToken cancellationToken)
        //  {
        /*  var query = Context.Set<PartnerPromoCodeLimit>().AsQueryable();
          var oldPartnerPromoCodeLimit = await query.SingleOrDefaultAsync(c => c.Id == partnerPromoCodeLimit.Id, cancellationToken);
          if (oldPartnerPromoCodeLimit != null)
          {
              if (oldPartnerPromoCodeLimit.CancelDate != partnerPromoCodeLimit.CancelDate)
                  oldPartnerPromoCodeLimit.CancelDate = partnerPromoCodeLimit.CancelDate;

              Context.PartnerPromoCodeLimits.Update(oldPartnerPromoCodeLimit);
              await Context.SaveChangesAsync(cancellationToken);
              return await GetByIdAsync(partnerPromoCodeLimit.Id, cancellationToken);
          }*/
        // return null;
        // }
    }
}
