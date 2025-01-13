namespace Infrastructure.Persistence.EFC.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
    void UpdateAsync(TEntity entity);
   void DeleteAsync(TEntity entity);
    
}