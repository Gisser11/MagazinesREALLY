using Microsoft.EntityFrameworkCore.Storage;
using Tech.Domain.Entity;
using Tech.Domain.Interfaces.Repositories;

namespace Tech.Domain.Interfaces.Databases;

public interface IUnitOfWork : IStateSaveChanges
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    
    IBaseRepository<User> Users { get; set; }
    
    IBaseRepository<Role> Roles { get; set; }
    
    IBaseRepository<UserRole> UserRoles { get; set; }
}