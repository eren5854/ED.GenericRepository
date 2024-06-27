using System.Linq.Expressions;

namespace ED.GenericRepository;
public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();

    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);

    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default, bool isTrackingActive = true);

    Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    bool Any(Expression<Func<TEntity, bool>> expression);

    TEntity GetByExpression(Expression<Func<TEntity, bool>> expression);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    void Add(TEntity entity);

    void Update(TEntity entity);

    Task DeleteByIdAsync(string id);

    void Delete(TEntity entity);
}
