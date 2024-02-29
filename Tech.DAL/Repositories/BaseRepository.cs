using Tech.Domain.Interfaces.Repositories;

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

    public async Task<int> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync();
    }
    
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException("Entity is null");
        
        await _db.AddAsync(entity);

        return entity;
    }

    
    public TEntity Update(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException("Entity is null");
        
        _db.Update(entity);

        return entity;
    }

    public TEntity Remove(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException("Entity is null");
        
        _db.Remove(entity);

        return entity;
    }
}