using Tech.Domain.Interfaces.Repostories;

namespace Tech.DAL.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _db;

    public BaseRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public IQueryable<TEntity> GetAll()
    {
        return _db.Set<TEntity>();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException("Entity is null");
        
        await _db.AddAsync(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    
    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException("Entity is null");
        
        _db.Update(entity);
        _db.SaveChanges();

        return Task.FromResult(entity);
    }

    public Task<TEntity> RemoveAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException("Entity is null");
        
        _db.Remove(entity);
        _db.SaveChanges();

        return Task.FromResult(entity);
    }
}