namespace GenericRepository.Core.Implementations
{
    public class GenericRepository<T>(DbContext context) : IRepository<T> where T : class
    {
        protected DbContext _context { get; set; } = context ?? throw new ArgumentNullException(nameof(context));
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public IQueryable<T> GetAll(bool asNoTracking = true)
            => asNoTracking ? _dbSet.AsNoTracking() : _dbSet;

        public async Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
            => await _dbSet.FindAsync(new[] { id }, cancellationToken);

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> predicate, bool asNoTracking = true)
            => asNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate);

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
            => await _dbSet.AnyAsync(predicate, cancellationToken);

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public void Update(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _dbSet.Remove(entity);
        }

        public async Task<PagedResult<T>> GetAllPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            // Guard clauses
            if (page <= 0) throw new ArgumentOutOfRangeException(nameof(page), "Page must be greater than 0.");
            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than 0.");

            var query = _dbSet.AsNoTracking();

            // Count total rows
            var totalCount = await query.CountAsync(cancellationToken);

            // Calculate paging
            var skip = (page - 1) * pageSize;
            var pageCount = (int)Math.Ceiling(totalCount / (double)pageSize);

            // Fetch paged data
            var items = await query
                .OrderBy(e => e) // Ensure there's an order by clause
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = totalCount,
                PageCount = pageCount,
                Results = items
            };
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> IsUniqueAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return !await _dbSet.AnyAsync(predicate, cancellationToken);
        }
    }
}
