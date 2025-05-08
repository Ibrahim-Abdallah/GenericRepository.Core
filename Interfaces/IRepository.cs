namespace GenericRepository.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll(bool asNoTracking = true);
        Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> predicate, bool asNoTracking = true);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        void Update(T entity);
        void Delete(T entity);

        Task<PagedResult<T>> GetAllPagedAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<bool> IsUniqueAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task BulkInsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task BulkUpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task BulkDeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    }
}
