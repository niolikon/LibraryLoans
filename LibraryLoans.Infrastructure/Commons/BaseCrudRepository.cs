using LibraryLoans.Core.Commons;
using LibraryLoans.Core.Exceptions;
using LibraryLoans.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryLoans.Infrastructure.Commons;

public class BaseCrudRepository<TEntity, Tid> : ICrudRepository<TEntity, Tid> where TEntity : BaseEntity<Tid>
{
    protected AppDbContext _dbContext;

    public BaseCrudRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<IEnumerable<TEntity>> ReadAllAsync()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> ReadAsync(Tid id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        TEntity? entityInDb = await _dbContext.Set<TEntity>().FindAsync(entity.Id);
        if (entityInDb == null)
        {
            throw new EntityNotFoundException("Could not fetch entity with specified id");
        }

        entityInDb.CopyFrom(entity);

        _dbContext.Set<TEntity>().Update(entityInDb);
        await _dbContext.SaveChangesAsync();

        return entityInDb;
    }

    public async Task DeleteAsync(Tid id)
    {
        TEntity? entity = await _dbContext.Set<TEntity>().FindAsync(id);
        if (entity == null)
        {
            throw new EntityNotFoundException("Could not fetch entity with specified id");
        }

        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
    }
}
