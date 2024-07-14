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
    public class CustomerPreferenceRepository : EfRepository<CustomerPreference>
    {
        public CustomerPreferenceRepository(DatabaseContext context) : base(context) { }

        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="customerpreference"> Сущность для добавления. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Добавленная сущность. </returns>
        public override async Task<CustomerPreference> AddAsync(CustomerPreference customerPreference, CancellationToken cancellationToken)
        {
            Context.CustomerPreferences.Add(customerPreference);
            await Context.SaveChangesAsync(cancellationToken);

            return await GetByIdAsync(customerPreference.Id, cancellationToken);
        }

        /// <summary>
        /// Добавить в базу массив сущностей.
        /// </summary>
        /// <param name="customerpreference"> Сущность для добавления. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Добавленные сущности. </returns>
        //public override async Task AddRangeAsync(ICollection<CustomerPreference> customerPreferences, CancellationToken cancellationToken)
        public override async Task AddRangeAsync(ICollection<CustomerPreference> customerPreferences, CancellationToken cancellationToken)
        {
            Context.CustomerPreferences.AddRange(customerPreferences);
            await Context.SaveChangesAsync(cancellationToken);
        }


        public override async Task<bool> DeleteRangeAsync(ICollection<CustomerPreference> customerPreferences, CancellationToken cancellationToken)
        {
            Context.CustomerPreferences.RemoveRange(customerPreferences);
            await Context.SaveChangesAsync(cancellationToken);
            return true;
        }




    }
}
