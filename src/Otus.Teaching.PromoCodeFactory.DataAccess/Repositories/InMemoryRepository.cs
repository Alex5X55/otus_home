using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>
        : IRepository<T>
        where T: IBaseEntity<T>
    {
        protected IEnumerable<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = data;
        }
        
        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id.Equals(id)));
        }

        public Task<string> DelByIdAsync(Guid id)
        {
            if (!Data.Any(x => x.Id.Equals(id))) return Task.FromResult("ERROR");
            
            Data = Data.Where(x => !x.Id.Equals(id));
            return Task.FromResult("OK");
        }


        public Task<string> UpdateAsync(T t)
        {
            var temp = Data.Where(x => x.Id.Equals(t.Id)).SingleOrDefault();

            temp = t;

            var tempList = Data.Where(x=>!x.Id.Equals(temp.Id)).ToList();
            tempList.Add(t);

            Data = tempList.AsEnumerable();
            return Task.FromResult("OK");

        }

        public Task<string> CreateAsync(T t)
        {
            var tempList = Data.ToList();
            tempList.Add(t);
            Data = tempList.AsEnumerable();
            return Task.FromResult("OK");

        }


    }
}