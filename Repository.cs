using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ED.GenericRepository;
public class Repository<TEntity, TContext> : IRepository<TEntity> where TEntity : class where TContext : DbContext
{
    private readonly TContext _context;
    private DbSet<TEntity> _entity;

    public Repository(TContext context)
    {
        _context = context;
        _entity = _context.Set<TEntity>();
    }
    public void Add(TEntity entity)
    {
        _entity.Add(entity);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _entity.AddAsync(entity, cancellationToken);
    }

    public bool Any(Expression<Func<TEntity, bool>> expression)
    {
        return _entity.Any(expression);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _entity.AnyAsync(expression, cancellationToken);
    }

    public void Delete(TEntity entity)
    {
        _entity.Remove(entity);
    }

    public async Task DeleteByIdAsync(string id)
    {
        TEntity entity = await _entity.FindAsync(id);
        _entity.Remove(entity);
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default, bool isTrackingActive = true)
    {
        return (!isTrackingActive) ? (await _entity.Where(expression).AsNoTracking().FirstOrDefaultAsync(cancellationToken)) : (await _entity.Where(expression).FirstOrDefaultAsync(cancellationToken));
    }

    public IQueryable<TEntity> GetAll()
    {
        return _entity.AsNoTracking().AsQueryable();
    }

    public TEntity GetByExpression(Expression<Func<TEntity, bool>> expression)
    {
        return _entity.Where(expression).AsNoTracking().FirstOrDefault();
    }

    public async Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _entity.Where(expression).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

    public void Update(TEntity entity)
    {
        _entity.Update(entity);
    }

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
    {
        return _entity.AsNoTracking().Where(expression).AsQueryable();
    }
}
