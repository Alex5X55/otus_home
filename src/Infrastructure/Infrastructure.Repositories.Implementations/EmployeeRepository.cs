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

        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="employee"> Сущность для добавления. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Добавленная сущность. </returns>
        public override async Task<Employee> AddAsync(Employee employee, CancellationToken cancellationToken)
        {
            Context.Employees.Add(employee);
            await Context.SaveChangesAsync(cancellationToken);

            return await GetByIdAsync(employee.Id, cancellationToken);
        }



        /// <summary>
        /// Получить список сущностей.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns> Список сотрудников. </returns>
        public override async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            var query = Context.Set<Employee>().AsQueryable();
            return await query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Получить сущность по ID.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Сотрудник. </returns>
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
                //var employee = await GetByIdAsync(id, cancellationToken);
                if (await query.Where(r => r.Id == id).ExecuteDeleteAsync(cancellationToken) == 0)
                //if (Context.Employes.Remove(role) == null)
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



        /// <summary>
        /// Получить постраничный список.
        /// </summary>
        /// <param name="filterDto"> ДТО фильтра. </param>
        /// <returns> Список курсов. </returns>
        //  public async Task<List<Employee>> GetPagedAsync(EmploeeFilterDto filterDto)
        //{
        /// var query = GetAll();
        //.Where(c => !c.Deleted);
        //.Include(c => c.Lessons).AsQueryable();
        ///   if (!string.IsNullOrWhiteSpace(filterDto.Name))
        ///   {
        ///      query = query.Where(c => c.Name == filterDto.Name);
        ///  }

        ///   if (filterDto.Price.HasValue)
        ///  {
        ///       query = query.Where(c => c.Price == filterDto.Price);
        ///   }

        ///  query = query
        ///     .Skip((filterDto.Page - 1) * filterDto.ItemsPerPage)
        ///     .Take(filterDto.ItemsPerPage);

        //  return new List<Employee>(); ////await query.ToListAsync();
        //}
    }
}