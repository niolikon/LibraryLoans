using System.Linq.Expressions;

namespace LibraryLoans.Core.Commons;

public interface ICrudRepository<TEntity, Tid> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity);

    Task<TEntity?> ReadAsync(Tid id);

    Task<IEnumerable<TEntity>> ReadAllAsync();

    Task<TEntity> UpdateAsync(TEntity entity);

    Task DeleteAsync(Tid id);

    Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate);
}
