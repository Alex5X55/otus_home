using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations
{
    public class CustomerRepository : EfRepository<Customer>
    {
        public CustomerRepository(DatabaseContext context) : base(context) { }

        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="customer"> Сущность для добавления. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Добавленная сущность. </returns>
        public override async Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken)
        {
            Context.Customers.Add(customer);
            await Context.SaveChangesAsync(cancellationToken);

            return await GetByIdAsync(customer.Id, cancellationToken);
        }

        public override async Task<Customer> UpdateAsync(Customer customer, CancellationToken cancellationToken)
        {
            var query = Context.Set<Customer>().AsQueryable();
            var oldCustomer = await query.SingleOrDefaultAsync(c => c.Id == customer.Id, cancellationToken);
            if (oldCustomer != null)
            {
                if (customer.FirstName != null)
                    oldCustomer.FirstName = customer.FirstName;
                if (customer.LastName != null)
                    oldCustomer.LastName = customer.LastName;
                if (customer.Email != null)
                    oldCustomer.Email = customer.Email;
                
                


                Context.Customers.Update(oldCustomer);
                await Context.SaveChangesAsync(cancellationToken);
                return await GetByIdAsync(customer.Id, cancellationToken);
            }

            return null;
        }








        /// <summary>
        /// Получить список сущностей.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns> Список сотрудников. </returns>
        public override async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            var query = Context.Set<Customer>().AsQueryable();
            return await query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Получить сущность по ID.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Сотрудник. </returns>
        public override async Task<Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = Context.Set<Customer>().AsQueryable();
            return await query.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public override async Task<Answer> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            Answer answer = new Answer
            {
                answerStatus = AnswerStatus.OK,
                text = $"Пользователь удален id: {id}",
            };

            try
            {
                var queryCustomerPreference = Context.Set<CustomerPreference>().AsQueryable();
                await queryCustomerPreference.Where(r => r.CustomerId == id).ExecuteDeleteAsync(cancellationToken);
                var queryCustomerPromoCode = Context.Set<CustomerPromoCode>().AsQueryable();
                await queryCustomerPromoCode.Where(r => r.CustomerId == id).ExecuteDeleteAsync(cancellationToken);
                var queryPromoCode = Context.Set<PromoCode>().AsQueryable();
                await queryPromoCode.Where(r => r.CustomerId == id).ExecuteDeleteAsync(cancellationToken);
                var queryCustomer = Context.Set<Customer>().AsQueryable();
                if (await queryCustomer.Where(r => r.Id == id).ExecuteDeleteAsync(cancellationToken) == 0)
                {
                    answer.answerStatus = AnswerStatus.NotFound;
                    answer.text = $"Пользователь id {id} не найден";
                }
            }
            catch (Exception e)
            {
                answer.answerStatus = AnswerStatus.NotFound;
                answer.text = e.Message;
            }

            await Context.SaveChangesAsync(cancellationToken);
            return answer;
        }





    }
}
