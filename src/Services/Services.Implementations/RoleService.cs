using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Services.Abstractions;
using Services.Contracts.Emploee;
using Services.Contracts.Role;
using Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Role> _roleRepository;

        public RoleService(
               IMapper mapper,
               IRepository<Role> roleRepository)
        // IBusControl busControl)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            //_busControl = busControl;
        }

        /// <summary>
        /// Получить списоку ролей.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО роли. </returns>
        public async Task<IEnumerable<RoleDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAllAsync(cancellationToken, true);
            var roleDtolList = roles.Select(x =>_mapper.Map<Role, RoleDto>(x)).ToList();

            return roleDtolList;
        }

        /// <summary>
        /// Получить роль.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> ДТО роли. </returns>
        public async Task<RoleDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<Role, RoleDto>(role);
        }

        /// <summary>
        /// Создать роль.
        /// </summary>
        /// <param name="creatingEmplooeDto"> ДТО создаваемого курса. </param>
        /// <returns> Идентификатор. </returns>
        public async Task<RoleDto> CreateAsync(CreatingRoleDto creatingRoleDto, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.AddAsync(_mapper.Map<CreatingRoleDto, Role>(creatingRoleDto), cancellationToken);
            return _mapper.Map<Role, RoleDto>(role); 
        }

        public async Task<RoleDto> UpdateAsync(UpdateRoleDto updateRoleDto, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.UpdateAsync(_mapper.Map<UpdateRoleDto, Role>(updateRoleDto), cancellationToken);
            return _mapper.Map<Role, RoleDto>(role); 
        }

        public async Task<Answer>DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _roleRepository.DeleteByIdAsync(id, cancellationToken);
        }

}
}
