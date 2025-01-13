using LibraryLoans.Core.BaseClasses;
using LibraryLoans.Core.BaseInterfaces;
using LibraryLoans.Core.Exceptions;
using LibraryLoans.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryLoans.Infrastructure.BaseClasses;

public class BaseRepository<TEntity, Tid> : IBaseRepository<TEntity, Tid> where TEntity : BaseEntity<Tid>
{
    protected AppDbContext _dbContext;

    public BaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetAsync(Tid id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.Set<TEntity>().Update(entity);

        int rowsAffected = await _dbContext.SaveChangesAsync();
        if (rowsAffected == 0)
        {
            throw new EntityNotFoundException("Could not fetch entity with specified id");
        }

        return entity;
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
