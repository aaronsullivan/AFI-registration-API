using Registration.Helpers.Domain;
using System.Linq.Expressions;

namespace Registration.Helpers.Repository;

public interface IRepository<TEntity>
        where TEntity : class, IAggregateRoot
{
    TEntity? GetById(int id);
    TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression);
    void Add(TEntity entity);
    void Remove(TEntity entity);
}