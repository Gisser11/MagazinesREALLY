using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tech.DAL.Interceptor;
using Tech.DAL.Repositories;
using Tech.Domain.Entity;
using Tech.Domain.Interfaces.Databases;
using Tech.Domain.Interfaces.Repositories;

namespace Tech.DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Sqlite");

        //services.AddSingleton<DateInterceptor>();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
        services.InitRepositories();
    }

    private static void InitRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWorks>();
        services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
        services.AddScoped<IBaseRepository<Role>, BaseRepository<Role>>();
        services.AddScoped<IBaseRepository<UserRole>, BaseRepository<UserRole>>();
        services.AddScoped<IBaseRepository<UserToken>, BaseRepository<UserToken>>();
        services.AddScoped<IBaseRepository<Article>, BaseRepository<Article>>();
        services.AddScoped<IBaseRepository<Author>, BaseRepository<Author>>();
    }
}