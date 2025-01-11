using System.Linq.Expressions;

namespace LibraryLoans.Core.BaseInterfaces;

public interface IBaseRepository<TEntity, Tid> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity);

    Task<TEntity?> GetAsync(Tid id);

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity> UpdateAsync(TEntity entity);

    Task DeleteAsync(Tid id);

    Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate);
}
