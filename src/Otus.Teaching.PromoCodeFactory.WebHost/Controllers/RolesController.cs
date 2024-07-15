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
        /// Получить список ролей
        /// </summary>
        /// <param name="СancellationToken">сancellationToken</param>
        /// <returns>Список ролей</returns>
        /// <remarks>
        /// Simple request:
        ///
        /// "https://localhost:9001/api/v1/Roles"
        ///
        /// </remarks>
        /// <response code="200">Получили список ролей</response>
        [HttpGet]
        public async Task<List<RoleModel>> GetRolesAsync(CancellationToken cancellationToken)
        {
            var rolesDto = await _roleService.GetAllAsync(cancellationToken);

            var rolessModelList = rolesDto.Select(x =>
             _mapper.Map<RoleDto, RoleModel>(x)).ToList();

            return rolessModelList;
        }

        /// <summary>
        /// Получить роль по идентификатору id
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="CancellationToken">cancellationToken</param>
        /// <returns>Роль</returns>
        /// <remarks>
        /// Simple request:
        ///
        /// "https://localhost:9001/api/v1/Roles/id"
        ///
        /// </remarks>
        /// <response code="200">Получили роль</response>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var roleDto = await _roleService.GetByIdAsync(id, cancellationToken);
            var roleShortResponse = _mapper.Map<RoleShortResponse>(roleDto);
            return Ok(roleShortResponse);
        }

        /// <summary>
        /// Создать новую роль
        /// </summary>
        /// <param name="newName">Фамилия</param>
        /// <param name="newDescription">Имя</param>
        /// <param name="CancellationToken">cancellationToken</param>
        /// <returns>Созданая роль</returns>
        /// <remarks>
        /// Simple request:
        /// POST
        /// "https://localhost:9001/api/v1/Roles/Name/Description"
        /// </remarks>
        /// <response code="200">Получили роль</response>
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
            return Ok(roleModel); 
           }

        /// <summary>
        /// Обновление данных роли
        /// </summary>
        /// <param name="roleid">Идентификатор</param>
        /// <param name="Name">Имя</param>
        /// <param name="Description">Описание</param>
        /// <param name="CancellationToken">cancellationToken</param>
        /// <returns>Обновленная роль</returns>
        /// <remarks>
        /// Simple request:
        /// UPDATE
        /// "https://localhost:9001/api/v1/Roles/Id/Name/Description"
        /// </remarks>
        /// <response code="200">Получили обновленного клиента</response>
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
 
            return Ok(_mapper.Map<RoleDto, RoleModel>(roleDto));
        }

        /// <summary>
        /// Удалить роль по идентификатору id
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="CancellationToken">cancellationToken</param>
        /// <returns>Роль</returns>
        /// <remarks>
        /// Simple request:
        /// DELETE
        ///
        /// "https://localhost:9001/api/v1/Roles/id"
        ///
        /// </remarks>
        /// <response code="200">Роль удалена</response>
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
        }
    }
}