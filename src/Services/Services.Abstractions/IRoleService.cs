using Services.Contracts.Emploee;
using Services.Contracts.Role;

namespace Services.Abstractions
{
    public interface IRoleService
    {
        /// <summary>
        /// Получить список ролей.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО роли. </returns>
        Task<IEnumerable<RoleDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить сотрудника.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО сотрудника. </returns>
        Task<RoleDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создать роль.
        /// </summary>
        /// <param name="creatingRoleDto"> ДТО создаваемой роли. </param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО нового сотрудника. </returns>
        Task<RoleDto> CreateAsync(CreatingRoleDto creatingRoleDto, CancellationToken cancellationToken);

        /// <summary>
        /// Изменить курс.
        /// </summary>
        /// <param name="id"> Иентификатор. </param>
        /// <param name="updatingRoleDto"> ДТО редактируемого курса. </param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task<RoleDto> UpdateAsync(UpdateRoleDto updateRoleDto, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить роль.
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
