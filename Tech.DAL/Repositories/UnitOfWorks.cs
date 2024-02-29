using Microsoft.EntityFrameworkCore.Storage;
using Tech.Domain.Entity;
using Tech.Domain.Interfaces.Databases;
using Tech.Domain.Interfaces.Repositories;

namespace Tech.DAL.Repositories;

public class UnitOfWorks : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWorks(ApplicationDbContext context, IBaseRepository<User> users, IBaseRepository<Role> roles, IBaseRepository<UserRole> userRoles)
    {
        _context = context;
        Users = users;
        Roles = roles;
        UserRoles = userRoles;
    }
    
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public IBaseRepository<User> Users { get; set; }
    
    public IBaseRepository<Role> Roles { get; set; }
    public IBaseRepository<UserRole> UserRoles { get; set; }
} 