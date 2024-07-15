using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Services.Abstractions;
using Services.Contracts.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Umbraco.Core.Models.Entities;

namespace Infrastructure.Repositories.Implementations
{
    public class RoleRepository : EfRepository<Role>
    {
        public RoleRepository(DatabaseContext context) : base(context) { }

        public override async Task<Role> AddAsync(Role role, CancellationToken cancellationToken)
        {
            Context.Roles.Add(role);
            await Context.SaveChangesAsync(cancellationToken);
            
            return await GetByIdAsync(role.Id, cancellationToken);
        }

        public override async Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            var query = Context.Set<Role>().AsQueryable();
            return await query.ToListAsync(cancellationToken);
        }

        public override async Task<Role> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = Context.Set<Role>().AsQueryable();
            return await query.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public override async Task<Role> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            var query = Context.Set<Role>().AsQueryable();
            var oldRole = await query.SingleOrDefaultAsync(c => c.Id == role.Id, cancellationToken);
            if (oldRole != null)
            {
                if (role.Name != null)
                    oldRole.Name = role.Name;
                if (role.Description != null)
                    oldRole.Description = role.Description;

                Context.Roles.Update(oldRole);
                await Context.SaveChangesAsync(cancellationToken);
                return await GetByIdAsync(role.Id, cancellationToken);
            }

            return null;
        }

        public override async Task<Answer> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            Answer answer = new Answer
            {
                answerStatus = AnswerStatus.OK,
                text = $"Роль удалена id: {id}",
            }; 

            try
                {
                 var query = Context.Set<Role>().AsQueryable();
                if (await query.Where(r => r.Id == id).ExecuteDeleteAsync(cancellationToken) == 0)
                {
                    answer.answerStatus = AnswerStatus.NotFound;
                    answer.text = $"Роль id {id} не найдена";
                }
            }
            catch (Exception e)
            {
                answer.answerStatus = AnswerStatus.NotFound;
                answer.text = e.Message;
            }

            await Context.SaveChangesAsync(cancellationToken);
            return answer; 
        }

    }
}
