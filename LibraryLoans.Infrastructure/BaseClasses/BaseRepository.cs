using LibraryLoans.Core.BaseClasses;
using LibraryLoans.Core.BaseInterfaces;
using LibraryLoans.Core.Exceptions;
using LibraryLoans.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryLoans.Infrastructure.BaseClasses;

public class BaseRepository<TEntity, Tid>(AppDbContext dbContext) : IBaseRepository<TEntity, Tid> where TEntity : BaseEntity<Tid>
{
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await dbContext.Set<TEntity>().AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetAsync(Tid id)
    {
        return await dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
        await dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(Tid id)
    {
        TEntity? entity = await dbContext.Set<TEntity>().FindAsync(id);
        if (entity == null)
        {
            throw new EntityNotFoundException("Could not fetch entity with specified id");
        }

        dbContext.Set<TEntity>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate)
    {
        return await dbContext.Set<TEntity>().Where(predicate).ToListAsync();
    }
}
