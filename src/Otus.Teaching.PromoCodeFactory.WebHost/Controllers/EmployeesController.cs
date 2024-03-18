using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

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
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeesController(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        
        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            var employeesModelList = employees.Select(x => 
                new EmployeeShortResponse()
                    {
                        Id = x.Id,
                        Email = x.Email,
                        FullName = x.FullName,
                    }).ToList();

            return employeesModelList;
        }
        
        /// <summary>
        /// Получить данные сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();
            
            var employeeModel = new EmployeeResponse()
            {
                Id = employee.Id,
                Email = employee.Email,
                Roles = employee.Roles.Select(x => new RoleItemResponse()
                {
                    Name = x.Name,
                    Description = x.Description
                }).ToList(),
                FullName = employee.FullName,
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };

            return employeeModel;
        }

        //Create/Delete/Update
        /// <summary>
        /// Удалить сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<string>> DeleteEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();


            var ret = await _employeeRepository.DelByIdAsync(id);
            if (ret.Equals("OK"))
                return Ok();

            return BadRequest();
        }

        /// <summary>
        /// Обновить данные сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}/{newName}/{newLastName}/{newEmail}/{AppliedPromocodesCount}")]
        public async Task<ActionResult<string>> UpdateEmployeeByIdAsync(Guid id, string newName, string newLastName, string newEmail, [FromBody]List<Role> newRoles, int AppliedPromocodesCount )
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null) 
                return NotFound();
            employee.FirstName = newName;
            employee.LastName = newLastName;
            employee.Email = newEmail;
            employee.Roles = new List<Role>(newRoles);
            employee.AppliedPromocodesCount = AppliedPromocodesCount;

            var ret = _employeeRepository.UpdateAsync(employee);
            if (ret.Equals("OK"))
                return Ok();

            return BadRequest();
        }

        /// <summary>
        /// Создать анкету сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpPost("{newName}/{newLastName}/{newEmail}/{AppliedPromocodesCount}")]
        public async Task<ActionResult<string>> CreateEmployeeAsync(string newName, string newLastName, string newEmail, [FromBody] List<Role> newRoles, int AppliedPromocodesCount)
        {

            var employee = new Employee { Id = Guid.NewGuid(), 
                                          FirstName = newName,
                                          LastName = newLastName,
                                          Email = newEmail,
                                          Roles = newRoles,
                AppliedPromocodesCount = AppliedPromocodesCount
            };

           // var ret = await _employeeRepository.CreateAsync(employee);
           // if (!ret.Equals("") { }

            return await _employeeRepository.CreateAsync(employee);
        }


    }
}