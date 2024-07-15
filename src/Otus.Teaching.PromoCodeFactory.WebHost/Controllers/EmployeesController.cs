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

        public EmployeesController(IEmployeeService service, ILogger<EmployeesController> logger, IMapper mapper)
        {
            _employeeService = service;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список сотрудников
        /// </summary>
        /// <param name="СancellationToken">сancellationToken</param>
        /// <returns>Список сотрудников</returns>
        /// <remarks>
        /// Simple request:
        ///
        /// "https://localhost:9001/api/v1/Emploees"
        ///
        /// </remarks>
        /// <response code="200">Получили список сотрудников</response>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync(CancellationToken cancellationToken)
        {
            var employeesDto = await _employeeService.GetAllAsync(cancellationToken);

            var employeesModelList = employeesDto.Select(x =>
             _mapper.Map<EmployeeDto, EmployeeShortResponse>(x)).ToList();

            return employeesModelList;
        }

        /// <summary>
        /// Получить сотрудника по идентификатору id
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="CancellationToken">cancellationToken</param>
        /// <returns>Сотрудник</returns>
        /// <remarks>
        /// Simple request:
        ///
        /// "https://localhost:9001/api/v1/Emploees/id"
        ///
        /// </remarks>
        /// <response code="200">Получили сотрудника</response>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var employeeDto = await _employeeService.GetByIdAsync(id, cancellationToken);
            var employeeResponse = _mapper.Map<EmployeeModel>(employeeDto);

            return Ok(employeeResponse);
        }

        /// <summary>
        /// Создать сотрудника
        /// </summary>
        /// <param name="newFirstName">Фамилия</param>
        /// <param name="newLastName">Имя</param>
        /// <param name="newEmail">Электронная почта</param>
        /// <param name="newRoleId">Роль</param>
        /// <param name="CancellationToken">cancellationToken</param>
        /// <returns>Созданый сотрудник</returns>
        /// <remarks>
        /// Simple request:
        /// POST
        /// "https://localhost:9001/api/v1/Emploees/FirstName/LastName/Email/RoleId"
        /// </remarks>
        /// <response code="200">Получили сотрудника</response>
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

            return Ok(employeeModel);
        }

        /// <summary>
        /// Обновление данных сотрудника
        /// </summary>
        /// <param name="UpdateEmployeeRequest">UpdateEmployeeRequest</param>
        /// <param name="CancellationToken">cancellationToken</param>
        /// <returns>Обновленный сотрудник</returns>
        /// <remarks>
        /// Simple request:
        /// UPDATE
        /// "https://localhost:9001/api/v1/Emploees"
        /// {
        ///  "id": "string",
        ///  "firstName": "string",
        ///  "lastName": "string",
        ///  "roleId": "string"
        /// }
        /// </remarks>
        /// <response code="200">Получили обновленного сотрудника</response>
        [HttpPut]
         public async Task<ActionResult<string>> UpdateEmployeeAsync(UpdateEmployeeRequest updateEmployeeRequest, CancellationToken cancellationToken)
        {
            var employeeDto = await _employeeService.UpdateAsync(_mapper.Map<UpdateEmployeeRequest, UpdateEmployeeDto>(updateEmployeeRequest), cancellationToken);

            if (employeeDto == null)
                return NotFound();
            return Ok(_mapper.Map<EmployeeDto, EmployeeModel>(employeeDto));
        }

        /// <summary>
        /// Удалить сотрудника по идентификатору id
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="CancellationToken">cancellationToken</param>
        /// <returns>Сотрудник</returns>
        /// <remarks>
        /// Simple request:
        /// DELETE
        ///
        /// "https://localhost:9001/api/v1/Emploees/id"
        ///
        /// </remarks>
        /// <response code="200">Сотрудник удален</response>
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
    }
}