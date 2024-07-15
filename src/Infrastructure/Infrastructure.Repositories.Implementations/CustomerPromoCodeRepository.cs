using Infrastructure.EntityFramework;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations
{
    public class CustomerPromoCodeRepository : EfRepository<CustomerPromoCode>
    {
        public CustomerPromoCodeRepository(DatabaseContext context) : base(context) { }

        public override async Task<CustomerPromoCode> AddAsync(CustomerPromoCode customerPromoCode, CancellationToken cancellationToken)
        {
            Context.CustomerPromoCode.Add(customerPromoCode);
            await Context.SaveChangesAsync(cancellationToken);

            return await GetByIdsAsync(customerPromoCode.CustomerId, customerPromoCode.PromoCodeId, cancellationToken);
        }
    }
}
