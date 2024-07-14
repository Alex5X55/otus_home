using Services.Contracts.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ICustomerService
    {
        /// <summary>
        /// Создать курс.
        /// </summary>
        /// <param name="creatingEmplooeDto"> ДТО создаваемого курса. </param>
        /// <returns> Идентификатор. </returns>
        Task<CustomersDto> CreateAsync(CreatingCustomerDto creatingCustomerDto, CancellationToken cancellationToken);
      //  Task<EmployeeDto> CreateAsync(CreatingEmployeeDto creatingEmployeeDto, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список клиентов.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО клиента. </returns>
        Task<IEnumerable<CustomerDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить клиента.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО клиента. </returns>
        Task<CustomersDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создать курс.
        /// </summary>
        /// <param name="creatingCourseDto"> ДТО создаваемого курса. </param>
        //  Task<Guid> CreateAsync(CreatingEmploeeDto creatingEmploeeDto);

        /// <summary>
        /// Изменить курс.
        /// </summary>
        /// <param name="id"> Иентификатор. </param>
        /// <param name="updatingCourseDto"> ДТО редактируемого курса. </param>
        Task<CustomersDto> UpdateAsync(UpdateCustomerDto updateCustomerDto, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task<Answer> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);


        /// <summary>
        /// Получить постраничный список.
        /// </summary>
        /// <param name="filterDto"> ДТО фильтра. </param>
        /// <returns> Список курсов. </returns>
      //  Task<ICollection<EmploeeDto>> GetPagedAsync(EmploeeFilterDto filterDto);
    }
}
