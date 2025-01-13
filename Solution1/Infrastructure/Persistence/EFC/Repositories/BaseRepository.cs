using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EFC.Repositories;

public class BaseRepository<TEntity>(AppDbContext context) : IBaseRepository<TEntity> where TEntity : class
{
    
    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await context.Set<TEntity>().AddAsync(entity);
    }

    public void UpdateAsync(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }

    public void DeleteAsync(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }
}