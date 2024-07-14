using AutoMapper;
using MassTransit;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Services.Abstractions;
using Services.Contracts.Emploee;
using Services.Contracts.Role;
using Services.Repositories.Abstractions;
using System.Threading;


namespace Services.Implementations
{
    /// <summary>
    /// Cервис работы с пользователями.
    /// </summary>
    public class EmploeeService : IEmployeeService
        {
            private readonly IMapper _mapper;
            private readonly IRepository<Employee> _employeeRepository;

        public EmploeeService(
                IMapper mapper,
                IRepository<Employee> employeeRepository)
               // IBusControl busControl)
            {
                _mapper = mapper;
                _employeeRepository = employeeRepository;
                //_busControl = busControl;
            }


        /// <summary>
        /// Создать курс.
        /// </summary>
        /// <param name="creatingEmplooeDto"> ДТО создаваемого курса. </param>
        /// <returns> Идентификатор. </returns>
        public async Task<EmployeeDto> CreateAsync(CreatingEmployeeDto creatingEmployeeDto, CancellationToken cancellationToken)
         {
            var employee = await _employeeRepository.AddAsync(_mapper.Map<CreatingEmployeeDto, Employee>(creatingEmployeeDto), cancellationToken);
            return _mapper.Map<Employee, EmployeeDto>(employee);
         }


        /// <summary>
        /// Получить списоку сотрудников.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО курса. </returns>
        public async Task<IEnumerable<EmployeeDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllAsync(cancellationToken, true);

            var employeesDtolList = employees.Select(x =>
               //new EmployeeDto()
               //{
                   _mapper.Map<Employee, EmployeeDto>(x)//employee)
                  // Id = x.Id,
                  // Email = x.Email,
                  // FullName = x.FullName,
               //}
               ).ToList();

            return employeesDtolList;
        }


        /// <summary>
        /// Получить сотрудника.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО курса. </returns>
        public async Task<EmployeeDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
            {
                var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);
                return _mapper.Map<Employee, EmployeeDto>(employee);
            }


        public async Task<EmployeeDto> UpdateAsync(UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken)
        {
            var role = await _employeeRepository.UpdateAsync(_mapper.Map<UpdateEmployeeDto, Employee>(updateEmployeeDto), cancellationToken);
            return _mapper.Map<Employee, EmployeeDto>(role);
        }


        public async Task<Answer> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _employeeRepository.DeleteByIdAsync(id, cancellationToken);
        }

        

        /// <summary>
        /// Изменить курс.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updateEmploeeDto"> ДТО редактируемого курса. </param>
        // public async Task UpdateAsync(Guid id, UpdateEmploeeDto updateEmploeeDto)
        // {
        //    var employee = await _employeeRepository.GetAsync(id, CancellationToken.None);
        //   if (employee == null)
        //   {
        //       throw new Exception($"Сотруник с идентфикатором {id} не найден");
        //   }

        //   employee.FirstName = updateEmploeeDto.FirstName;
        //  employee.LastName = updateEmploeeDto.LastName;
        //    _employeeRepository.Update(employee);
        //    await _employeeRepository.SaveChangesAsync();
        // }

        /// <summary>
        /// Удалить курс.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        // public async Task DeleteAsync(Guid id)
        //{
        //     var employee = await _employeeRepository.GetAsync(id, CancellationToken.None);
        //    employee.Deleted = true;
        //     await _employeeRepository.SaveChangesAsync();
        // }

        /// <summary>
        /// Получить постраничный список.
        /// </summary>
        /// <param name="filterDto"> ДТО фильтра. </param>
        /// <returns> Список курсов. </returns>
        // public async Task<ICollection<EmploeeDto>> GetPagedAsync(EmploeeFilterDto filterDto)
        // {
        //     ICollection<Employee> entities = await _employeeRepository.GetPagedAsync(filterDto);
        //     return _mapper.Map<ICollection<Employee>, ICollection<EmploeeDto>>(entities);
        // }
    }
}
