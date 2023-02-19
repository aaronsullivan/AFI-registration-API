using Microsoft.EntityFrameworkCore;
using Registration.Helpers.Domain;
using Registration.Helpers.Repository;
using System.Linq.Expressions;

namespace Registration.Infrastructure;

public class DatabaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IAggregateRoot
{
    private readonly DatabaseContext _databaseContext;
    private readonly DbSet<TEntity> _dbSet;

    public DatabaseRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _dbSet = _databaseContext.Set<TEntity>();
    }

    public TEntity? GetById(int id)
    {
        return _dbSet.FirstOrDefault(x => x.Id == id);
    }

    public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression)
    {
        return _dbSet.FirstOrDefault(expression);
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
}