﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Services.Repositories.Abstractions;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain;
using Infrastructure.EntityFramework;
using Services.Abstractions;

namespace Infrastructure.Repositories.Implementations
{
    /// <summary>
    /// Репозиторий.
    /// </summary>
    /// <typeparam name="T"> Тип сущности. </typeparam>
    /// <typeparam name="Guid"> Тип первичного ключа. </typeparam>
    public class EfRepository<T> : IRepository<T> where T
        : class, IBaseEntity<Guid>
    {
        protected readonly DatabaseContext Context;
        private readonly DbSet<T> _entitySet;

        public EfRepository(DatabaseContext context)
        {
            Context = context;
            _entitySet = Context.Set<T>();
        }

        #region Get

        /// <summary>
        /// Получить сущность по ID.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <returns> Cущность. </returns>
        public virtual T Get(Guid id)
        {
            return _entitySet.Find(id);
        }

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Cущность. </returns>
        public virtual async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _entitySet.FindAsync((object)id);
        }

        public virtual async Task<T> GetByIdsAsync(Guid id, Guid id2, CancellationToken cancellationToken)
        {
            return await _entitySet.FindAsync((object)id, (object)id2);
        }


        #endregion

        #region GetAll

        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="asNoTracking"> Вызвать с AsNoTracking. </param>
        /// <returns> IQueryable массив сущностей. </returns>
        public virtual IQueryable<T> GetAll(bool asNoTracking = false)
        {
            return asNoTracking ? _entitySet.AsNoTracking() : _entitySet;
        }

        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <param name="asNoTracking"> Вызвать с AsNoTracking. </param>
        /// <returns> Список сущностей. </returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            return await GetAll().ToListAsync(cancellationToken);
        }


        #endregion

        #region Create

        /// <summary>
        /// Добавить в базу сущность.
        /// </summary>
        /// <param name="entity"> Cущность для добавления. </param>
        /// <returns>. Добавленная сущность. </returns>
        public virtual T Add(T entity)
        {
            var objToReturn = _entitySet.Add(entity);
            return objToReturn.Entity;
        }

        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="entity"> Сущность для добавления. </param>
        /// <returns> Добавленная сущность. </returns>
        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            return (await _entitySet.AddAsync(entity)).Entity;
        }

        /// <summary>
        /// Добавить в базу массив сущностей.
        /// </summary>
        /// <param name="entities"> Массив сущностей. </param>
        public virtual void AddRange(List<T> entities)
        {
            var enumerable = entities as IList<T> ?? entities.ToList();
            _entitySet.AddRange(enumerable);
        }

        /// <summary>
        /// Добавить в базу массив сущностей.
        /// </summary>
        /// <param name="entities"> Массив сущностей. </param>
        public virtual async Task AddRangeAsync(ICollection<T> entities, CancellationToken cancellationToken)
        {
            if (entities == null || !entities.Any())
            {
                return;
            }
            await _entitySet.AddRangeAsync(entities);
        }

        #endregion

        #region Update

        /// <summary>
        /// Для сущности проставить состояние - что она изменена.
        /// </summary>
        /// <param name="entity"> Сущность для изменения. </param>
        public virtual async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await _entitySet.ExecuteUpdateAsync<T>(e => e.SetProperty(a => a.Id, a => a.Id));
            return entity;
        }

        /// <summary>
        /// Для сущности проставить состояние - что она изменена.
        /// </summary>
        /// <param name="entity"> Сущность для изменения. </param>
        public virtual async Task<T> UpdateCancelAsync(T entity, CancellationToken cancellationToken)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await _entitySet.ExecuteUpdateAsync<T>(e => e.SetProperty(a => a.Id, a => a.Id));
            return entity;
        }

        #endregion

        #region Delete

        public virtual async Task<Answer> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            await _entitySet.FindAsync(1);
            return new Answer {answerStatus = AnswerStatus.OK };
        }

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="id"> Id удалённой сущности. </param>
        /// <returns> Была ли сущность удалена. </returns>
        public virtual bool Delete(Guid id)
        {
            var obj = _entitySet.Find(id);
            if (obj == null)
            {
                return false;
            }
            _entitySet.Remove(obj);
            return true;
        }

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="entity"> Сущность для удаления. </param>
        /// <returns> Была ли сущность удалена. </returns>
        public virtual bool Delete(T entity)
        {
            if (entity == null)
            {
                return false;
            }
            Context.Entry(entity).State = EntityState.Deleted;
            return true;
        }

        /// <summary>
        /// Удалить сущности.
        /// </summary>
        /// <param name="entities"> Коллекция сущностей для удаления. </param>
        /// <returns> Была ли операция завершена успешно. </returns>
        public virtual async Task<bool> DeleteRangeAsync(ICollection<T> entities, CancellationToken cancellationToken)
        {
            if (entities == null || !entities.Any())
            {
                return false;
            }
            _entitySet.RemoveRange(entities);
            return true;
        }

        #endregion

        #region SaveChanges

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        public virtual void SaveChanges()
        {
            Context.SaveChanges();
        }

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}
