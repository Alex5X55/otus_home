using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Services.Abstractions;
using Services.Contracts.Emploee;
using Services.Repositories.Abstractions;
using Umbraco.Core;

namespace Infrastructure.Repositories.Implementations
{
    /// <summary>
    /// Репозиторий работы с сотрудниками.
    /// </summary>
    public class EmployeeRepository : EfRepository<Employee>
    {
        public EmployeeRepository(DatabaseContext context) : base(context){ }

        public override async Task<Employee> AddAsync(Employee employee, CancellationToken cancellationToken)
        {
            Context.Employees.Add(employee);
            await Context.SaveChangesAsync(cancellationToken);

            return await GetByIdAsync(employee.Id, cancellationToken);
        }

        public override async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            var query = Context.Set<Employee>().AsQueryable();
            return await query.ToListAsync(cancellationToken);
        }

        public override async Task<Employee> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = Context.Set<Employee>().AsQueryable();
            return await query.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public override async Task<Employee> UpdateAsync(Employee employee, CancellationToken cancellationToken)
        {
            var query = Context.Set<Employee>().AsQueryable();
            var oldEmployee = await query.SingleOrDefaultAsync(c => c.Id == employee.Id, cancellationToken);
            if (oldEmployee != null)
            {
                if (employee.FirstName != null)
                    oldEmployee.FirstName = employee.FirstName;
                if (employee.LastName != null)
                    oldEmployee.LastName = employee.LastName;
                if (employee.Email != null)
                    oldEmployee.Email = employee.Email;
                if (employee.RoleId != null)
                    oldEmployee.RoleId = employee.RoleId;

                Context.Employees.Update(oldEmployee);
                await Context.SaveChangesAsync(cancellationToken);
                return await GetByIdAsync(employee.Id, cancellationToken);
            }

            return null;
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
                var query = Context.Set<Employee>().AsQueryable();
                if (await query.Where(r => r.Id == id).ExecuteDeleteAsync(cancellationToken) == 0)
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