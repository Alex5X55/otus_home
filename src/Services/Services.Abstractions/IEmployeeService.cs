
using Services.Contracts.Emploee;
using System.Threading;


namespace Services.Abstractions
{
    /// <summary>
    /// Интерфейс сервиса работы с сотрудниками.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Создать курс.
        /// </summary>
        /// <param name="creatingEmplooeDto"> ДТО создаваемого курса. </param>
        /// <returns> Идентификатор. </returns>
        Task<EmployeeDto> CreateAsync(CreatingEmployeeDto creatingEmployeeDto, CancellationToken cancellationToken);
        
        /// <summary>
        /// Получить список сотрудников.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО сотрудника. </returns>
        Task<IEnumerable<EmployeeDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить сотрудника.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО сотрудника. </returns>
        Task<EmployeeDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

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
        Task<EmployeeDto> UpdateAsync(UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken);

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