using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Services.Abstractions;
using Services.Contracts.Customer;
using Services.Contracts.CustomerPreference;
using Services.Contracts.CustomerPromoCode;
using Services.Repositories.Abstractions;

namespace Services.Implementations
{

    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerPreference> _customerPreferenceRepository;

        public CustomerService(
                IMapper mapper,
                IRepository<Customer> customerRepository,
                IRepository<CustomerPreference> customerPreferenceRepository
                )
        // IBusControl busControl)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _customerPreferenceRepository = customerPreferenceRepository;
            //_busControl = busControl;
        }


        /// <summary>
        /// Создать курс.
        /// </summary>
        /// <param name="creatingEmplooeDto"> ДТО создаваемого курса. </param>
        /// <returns> Идентификатор. </returns>
        public async Task<CustomersDto> CreateAsync(CreatingCustomerDto creatingCustomerDto, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.AddAsync(_mapper.Map<CreatingCustomerDto, Customer>(creatingCustomerDto), cancellationToken);
            if((customer != null) && (creatingCustomerDto.PreferenceIds != null) && (creatingCustomerDto.PreferenceIds.Count > 0))
            {
                var customerPreferences = new List<CustomerPreference>();
                foreach (var id in creatingCustomerDto.PreferenceIds)
                {
                    customerPreferences.Add(new CustomerPreference()
                    {
                        Id = Guid.NewGuid(),
                        CustomerId = customer.Id,
                        PreferenceId = id
                    });
                }
                await _customerPreferenceRepository.AddRangeAsync((ICollection<CustomerPreference>)customerPreferences, cancellationToken);
            }
            
            
            var customersDto = new CustomersDto();
            customersDto.Id = customer.Id;
            customersDto.FirstName = customer.FirstName;
            customersDto.LastName = customer.LastName;
            customersDto.Email = customer.Email;
            if(customer.Preferences != null)
                customersDto.Preferences = customer.Preferences.Select(x =>
            _mapper.Map<CustomerPreference, CustomerPreferenceDto>(x)).ToList();

            return customersDto;    //_mapper.Map<Customer, CustomersDto>(customer);
        }

        public async Task<CustomersDto> UpdateAsync(UpdateCustomerDto updateCustomerDto, CancellationToken cancellationToken)
        {
            var oldCustomer = await _customerRepository.GetByIdAsync(updateCustomerDto.Id, cancellationToken);
            var customersDto = new CustomersDto();
            if (oldCustomer == null) return customersDto;

            if ((oldCustomer != null) && (oldCustomer.Preferences != null) && (oldCustomer.Preferences.Count > 0))
            {
                var preferenceIds = new List<Guid?>();
                foreach (var preference in oldCustomer.Preferences)
                    preferenceIds.Add(preference.PreferenceId);

                if (!Common.ComapreLists(preferenceIds, updateCustomerDto.PreferenceIds))
                {
                    if (await _customerPreferenceRepository.DeleteRangeAsync((ICollection<CustomerPreference>)oldCustomer.Preferences, cancellationToken))
                    {
                        var customerPreferences = new List<CustomerPreference>();
                        foreach (var id in updateCustomerDto.PreferenceIds)
                        {
                            customerPreferences.Add(new CustomerPreference()
                            {
                                Id = Guid.NewGuid(),
                                CustomerId = updateCustomerDto.Id,
                                PreferenceId = id
                            });
                        }
                        await _customerPreferenceRepository.AddRangeAsync((ICollection<CustomerPreference>)customerPreferences, cancellationToken);
                    }

                }
            
                var customer = await _customerRepository.UpdateAsync(_mapper.Map<UpdateCustomerDto, Customer>(updateCustomerDto), cancellationToken);

                
                customersDto.Id = customer.Id;
                customersDto.FirstName = customer.FirstName;
                customersDto.LastName = customer.LastName;
                customersDto.Email = customer.Email;
                if (customer.Preferences != null)
                    customersDto.Preferences = customer.Preferences.Select(x =>
                _mapper.Map<CustomerPreference, CustomerPreferenceDto>(x)).ToList();
            }
            return customersDto;    //_mapper.Map<Customer, CustomersDto>(customer);
        }


        /// <summary>
        /// Получить списоку сотрудников.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО курса. </returns>
        public async Task<IEnumerable<CustomerDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync(cancellationToken, true);

            var customersDtoList = customers.Select(x =>
                   //new EmployeeDto()
                   //{
                   _mapper.Map<Customer, CustomerDto>(x)//employee)
                                                        // Id = x.Id,
                                                        // Email = x.Email,
                                                        // FullName = x.FullName,
                                                        //}
               ).ToList();

            return customersDtoList;
        }


        /// <summary>
        /// Получить сотрудника.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО курса. </returns>
        public async Task<CustomersDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);

            var customersDto = new CustomersDto();
                customersDto.Id = customer.Id;
                customersDto.FirstName = customer.FirstName;
                customersDto.LastName = customer.LastName;
                customersDto.Email = customer.Email;
            //customersDto.Preferences = new List<CustomerPreferenceDto>();
            //foreach (var customerpreference in customer.Preferences)
            //customersDto.Preferences.Add(_mapper.Map<CustomerPreference, CustomerPreferenceDto>(customerpreference));
                if((customer.Preferences != null) && (customer.Preferences.Count > 0))
                  customersDto.Preferences = customer.Preferences.Select(x=>
                _mapper.Map<CustomerPreference, CustomerPreferenceDto>(x)).ToList();
                if ((customer.PromoCodes != null) && (customer.PromoCodes.Count > 0))
                  customersDto.PromoCodes = customer.PromoCodes.Select(x =>
                _mapper.Map<CustomerPromoCode, CustomerPromoCodeDto>(x)).ToList();


            // public List<PreferenceDto>? Preferences { get; set; }
            // public List<PromoCodeDto>? PromoCodes { get; set; }


            return customersDto; //_mapper.Map<Customer, CustomersDto>(customer);
        }


        //   public async Task<EmployeeDto> UpdateAsync(UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken)
        //   {
        //       var role = await _employeeRepository.UpdateAsync(_mapper.Map<UpdateEmployeeDto, Employee>(updateEmployeeDto), cancellationToken);
        //       return _mapper.Map<Employee, EmployeeDto>(role);
        //   }


        //   public async Task<Answer> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        //  {
        //      return await _employeeRepository.DeleteByIdAsync(id, cancellationToken);
        //  }



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
        public async Task<Answer> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _customerRepository.DeleteByIdAsync(id, cancellationToken);
        }

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
