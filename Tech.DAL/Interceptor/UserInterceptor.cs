using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Tech.Domain.Entity;

namespace Tech.DAL.Interceptor;

/// <summary>
/// 
/// </summary>
public class UserSaveChangesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var entries = eventData.Context.ChangeTracker.Entries<IdentityUser>().ToList();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                var user = new User { IdentityId = entry.Entity.Id };
                eventData.Context.Add(user); 
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}