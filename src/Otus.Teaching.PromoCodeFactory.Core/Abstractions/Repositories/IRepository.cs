using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Domain;

namespace Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories
{
    public interface IRepository<T>
        where T: IBaseEntity<Guid>
    {
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        
        Task<string> DelByIdAsync(Guid id);

        Task<string> UpdateAsync(T t);

        Task<string> CreateAsync(T t);

    }
}