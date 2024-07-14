using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Emploee;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Role;
using Services.Abstractions;
using Services.Contracts.Role;
using Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Роли сотрудников
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RolesController 
        : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly ILogger<RolesController> _logger;

        public RolesController(IRoleService service, ILogger<RolesController> logger, IMapper mapper)
        {
            _roleService = service;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Создать роль
        /// </summary>
        /// <returns></returns>
        [HttpPost("{newName}/{newDescription}")]
        public async Task<ActionResult<string>> CreateRoleAsync(string newName, string newDescription, CancellationToken cancellationToken)
           {
            var creatingRoleDto = new CreatingRoleDto
            {
                Name = newName,
                Description = newDescription,
            };
                
            var roleDto = await _roleService.CreateAsync(creatingRoleDto, cancellationToken);
            var roleModel = _mapper.Map<RoleDto, RoleModel>(roleDto);

            //      var employee = new Employee { Id = Guid.NewGuid(), 
            //                                   FirstName = newName,
            //                                   LastName = newLastName,
            //                                   Email = newEmail,
            //                                    Roles = newRoles,
            //          AppliedPromocodesCount = AppliedPromocodesCount
            //       };

            // var ret = await _employeeRepository.CreateAsync(employee);
            // if (!ret.Equals("") { }

            return Ok(roleModel); 
           }

        /// <summary>
        /// Получить все доступные роли сотрудников
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<RoleModel>> GetRolesAsync(CancellationToken cancellationToken)
        {
            var rolesDto = await _roleService.GetAllAsync(cancellationToken);

            var rolessModelList = rolesDto.Select(x =>
             _mapper.Map<RoleDto, RoleModel>(x)).ToList();

            return rolessModelList;
        }

        /// <summary>
        /// Получить данные роли по Id
        /// </summary>
        /// <param name="id">Id роли</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var roleDto = await _roleService.GetByIdAsync(id, cancellationToken);
            var roleShortResponse = _mapper.Map<RoleShortResponse>(roleDto);
            return Ok(roleShortResponse);
        }

        [HttpPut("{roleid}/{newName}/{newDescription}")]
        public async Task<ActionResult<string>> UpdateRoleAsync(Guid roleid, string newName, string newDescription, CancellationToken cancellationToken)
           {
            var updateRoleDto = new UpdateRoleDto
            {
                Id = roleid,
                Name = newName,
                Description = newDescription,
            };
            var roleDto = await _roleService.UpdateAsync(updateRoleDto, cancellationToken);

            if (roleDto == null) 
                 return NotFound();
        //     employee.FirstName = newName;
        //     employee.LastName = newLastName;
        //     employee.Email = newEmail;
        //      employee.Roles = new List<Role>(newRoles);
        //      employee.AppliedPromocodesCount = AppliedPromocodesCount;

        //     var ret = _employeeRepository.UpdateAsync(employee);
        //     if (ret.Equals("OK"))
            return Ok(_mapper.Map<RoleDto, RoleModel>(roleDto));

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

        /// <summary>
        /// Удалить роль по Id
        /// <param name="Guid">Cancellation token</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<string>> DeleteRoleByIdAsync(Guid id, CancellationToken cancellationToken)
           {
            var answer = await _roleService.DeleteByIdAsync(id, cancellationToken);
            
            return answer.answerStatus switch
            {
                AnswerStatus.OK => Ok(answer.text),
                AnswerStatus.Error => Problem(answer.text),
                AnswerStatus.NotFound => NotFound(answer.text),
                _ => BadRequest("Неизвестная ошибка")
            };



            
        //      var employee = await _employeeRepository.GetByIdAsync(id);

        //       if (employee == null)
        //         return NotFound();


        //     var ret = await _employeeRepository.DelByIdAsync(id);
        //    if (ret.Equals("OK"))
        //        return Ok();

        //     return BadRequest();
           }

        /// <summary>
        /// Обновить данные сотрудника по Id
        /// </summary>
        /// <returns></returns>

    }
}