using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Emploee;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Role;
using Services.Abstractions;
using Services.Contracts.Emploee;
using Services.Contracts.Role;
using Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController
        : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeesController> _logger;

       // public EmployeesController(IRepository<Employee> employeeRepository)
       // {
         //   _employeeRepository = employeeRepository;
       // }

        public EmployeesController(IEmployeeService service, ILogger<EmployeesController> logger, IMapper mapper)
        {
            _employeeService = service;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Создать сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpPost("{newFirstName}/{newLastName}/{newEmail}/{newRoleId}")]
        public async Task<ActionResult<string>> CreateEmployeeAsync(string newFirstName, string newLastName, string newEmail, Guid newRoleId, CancellationToken cancellationToken)
        {
            var creatingEmployeeDto = new CreatingEmployeeDto
            {
                FirstName = newFirstName,
                LastName = newLastName,
                Email = newEmail,
                RoleId = newRoleId,
            };

            var employeeDto = await _employeeService.CreateAsync(creatingEmployeeDto, cancellationToken);
            var employeeModel = _mapper.Map<EmployeeDto, EmployeeModel>(employeeDto);

            //      var employee = new Employee { Id = Guid.NewGuid(), 
            //                                   FirstName = newName,
            //                                   LastName = newLastName,
            //                                   Email = newEmail,
            //                                    Roles = newRoles,
            //          AppliedPromocodesCount = AppliedPromocodesCount
            //       };

            // var ret = await _employeeRepository.CreateAsync(employee);
            // if (!ret.Equals("") { }

            return Ok(employeeModel);
        }

       /// <summary>
       /// Получить данные всех сотрудников
       /// </summary>
       /// <param name="cancellationToken">Cancellation token</param>
       /// <returns></returns>
       [HttpGet]
       public async Task<List<EmployeeShortResponse>> GetEmployeesAsync(CancellationToken cancellationToken)
        {
            var employeesDto = await _employeeService.GetAllAsync(cancellationToken);

            var employeesModelList = employeesDto.Select(x =>
             _mapper.Map<EmployeeDto, EmployeeShortResponse>(x)).ToList();

            return employeesModelList;
        }

        /// <summary>
        /// Получить данные сотрудника по Id
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var employeeDto = await _employeeService.GetByIdAsync(id, cancellationToken);
            var employeeResponse = _mapper.Map<EmployeeModel>(employeeDto);
            
            return Ok(employeeResponse);



            //var employee = await _employeeRepository.GetByIdAsync(id);

            //if (employee == null)
            //     return NotFound();

            // var employeeModel = new GetEmployeeResponse()
            // {
            //     Id = employee.Id,
            ////     Email = employee.Email,
            //    Roles = employee.Roles.Select(x => new RoleModel()
            //    {
            //        Name = x.Name,
            //        Description = x.Description
            //    }).ToList(),
            //    FullName = employee.FullName,
            //    AppliedPromocodesCount = employee.AppliedPromocodesCount
            //  };

            // return employeeModel;
            //return Ok(_mapper.Map<GetEmployeeResponse>(await _employeeService.GetByIdAsync(id)));
        }
                
        [HttpPut]
         public async Task<ActionResult<string>> UpdateEmployeeAsync(UpdateEmployeeRequest updateEmployeeRequest, CancellationToken cancellationToken)
        {
            var employeeDto = await _employeeService.UpdateAsync(_mapper.Map<UpdateEmployeeRequest, UpdateEmployeeDto>(updateEmployeeRequest), cancellationToken);

            if (employeeDto == null)
                return NotFound();
            //     employee.FirstName = newName;
            //     employee.LastName = newLastName;
            //     employee.Email = newEmail;
            //      employee.Roles = new List<Role>(newRoles);
            //      employee.AppliedPromocodesCount = AppliedPromocodesCount;

            //     var ret = _employeeRepository.UpdateAsync(employee);
            //     if (ret.Equals("OK"))
            return Ok(_mapper.Map<EmployeeDto, EmployeeModel>(employeeDto));

            //      return BadRequest();



            //      var employee = new Employee { Id = Guid.NewGuid(), 
            //                                   FirstName = newName,
            //                                   LastName = newLastName,
            //                                   Email = newEmail,
            //                                    Roles = newRoles,
            //          AppliedPromocodesCount = AppliedPromocodesCount
            //       };

            // var ret = await _employeeRepository.CreateAsync(employee);
            // if (!ret.Equals("") { }
        }



        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<string>> DeleteEmploeeByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var answer = await _employeeService.DeleteByIdAsync(id, cancellationToken);

            return answer.answerStatus switch
            {
                AnswerStatus.OK => Ok(answer.text),
                AnswerStatus.Error => Problem(answer.text),
                AnswerStatus.NotFound => NotFound(answer.text),
                _ => BadRequest("Неизвестная ошибка")
            };
        }

            //Create/Delete/Update
            /// <summary>
            /// Удалить сотрудника по Id
            /// </summary>
            /// <returns></returns>
            //    [HttpDelete("{id:guid}")]
            //   public async Task<ActionResult<string>> DeleteEmployeeByIdAsync(Guid id)
            //   {
            //      var employee = await _employeeRepository.GetByIdAsync(id);

            //       if (employee == null)
            //         return NotFound();


            //     var ret = await _employeeRepository.DelByIdAsync(id);
            //    if (ret.Equals("OK"))
            //        return Ok();

            //     return BadRequest();
            //   }

            /// <summary>
            /// Обновить данные сотрудника по Id
            /// </summary>
            /// <returns></returns>
            //   [HttpPut("{id}/{newName}/{newLastName}/{newEmail}/{AppliedPromocodesCount}")]
            //   public async Task<ActionResult<string>> UpdateEmployeeByIdAsync(Guid id, string newName, string newLastName, string newEmail, [FromBody]List<Role> newRoles, int AppliedPromocodesCount )
            //   {
            //       var employee = await _employeeRepository.GetByIdAsync(id);

            //     if (employee == null) 
            //         return NotFound();
            //     employee.FirstName = newName;
            //     employee.LastName = newLastName;
            //     employee.Email = newEmail;
            //      employee.Roles = new List<Role>(newRoles);
            //      employee.AppliedPromocodesCount = AppliedPromocodesCount;

            //     var ret = _employeeRepository.UpdateAsync(employee);
            //     if (ret.Equals("OK"))
            //          return Ok();

            //      return BadRequest();
            //   }

            /// <summary>
            /// Создать анкету сотрудника
            /// </summary>
            /// <returns></returns>
            // [HttpPost("{newName}/{newLastName}/{newEmail}/{AppliedPromocodesCount}")]
            // public async Task<ActionResult<string>> CreateEmployeeAsync(string newName, string newLastName, string newEmail, [FromBody] List<Role> newRoles, int AppliedPromocodesCount)
            //   {

            //      var employee = new Employee { Id = Guid.NewGuid(), 
            //                                   FirstName = newName,
            //                                   LastName = newLastName,
            //                                   Email = newEmail,
            //                                    Roles = newRoles,
            //          AppliedPromocodesCount = AppliedPromocodesCount
            //       };

            // var ret = await _employeeRepository.CreateAsync(employee);
            // if (!ret.Equals("") { }

            //       return await _employeeRepository.CreateAsync(employee);
            //   }


        }
}