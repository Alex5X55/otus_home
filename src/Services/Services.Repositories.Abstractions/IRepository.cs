using Otus.Teaching.PromoCodeFactory.Core.Domain;
using Services.Abstractions;


namespace Services.Repositories.Abstractions
{ /// <summary>
  /// Описания общих методов для всех репозиториев.
  /// </summary>
  /// <typeparam name="T"> Тип Entity для репозитория. </typeparam>
  /// <typeparam name="Guid"> Тип первичного ключа. </typeparam>
    public interface IRepository<T>
        where T : IBaseEntity<Guid>
    {
        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="noTracking"> Вызвать с AsNoTracking.</param>
        /// <returns> IQueryable массив сущностей.</returns>
        IQueryable<T> GetAll(bool noTracking = false);

        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены. </param>
        /// <param name="asNoTracking"> Вызвать с AsNoTracking. </param>
        /// <returns> Список сущностей. </returns>
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false);

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <returns> Cущность. </returns>
        T Get(Guid id);

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Cущность. </returns>
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="id"> Id удалённой сущности. </param>
        /// <returns> Была ли сущность удалена. </returns>
        Task<Answer> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="entity"> Cущность для удаления. </param>
        /// <returns> Была ли сущность удалена. </returns>
        bool Delete(T entity);

        /// <summary>
        /// Удалить сущности.
        /// </summary>
        /// <param name="entities"> Коллекция сущностей для удаления. </param>
        /// <returns> Была ли операция удаления успешна. </returns>
        Task<bool> DeleteRangeAsync(ICollection<T> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Для сущности проставить состояние - что она изменена.
        /// </summary>
        /// <param name="entity"> Сущность для изменения. </param>
        //void Update(T entity);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="entity"> Сущность для добавления. </param>
        /// <returns> Добавленная сущность. </returns>
        T Add(T entity);

        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="entity"> Сущность для добавления. </param>
        /// <returns> Добавленная сущность. </returns>
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Добавить в базу массив сущностей.
        /// </summary>
        /// <param name="entities"> Массив сущностей. </param>
        void AddRange(List<T> entities);

        /// <summary>
        /// Добавить в базу массив сущностей.
        /// </summary>
        /// <param name="entities"> Массив сущностей. </param>
        Task AddRangeAsync(ICollection<T> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
