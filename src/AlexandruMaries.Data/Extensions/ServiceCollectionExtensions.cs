using AlexandruMaries.Data.Interfaces;
using AlexandruMaries.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AlexandruMaries.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, string connectionString)
    {
        return services.AddDbContext<AlexandruMariesDbContext>(options =>
            options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("AlexandruMaries.Data")
            ))
            .AddSingleton<IDapperContext>(_ => new DapperContext(connectionString))
            .AddScoped<IReferenceRepository, ReferenceRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IViewsRepository, ViewsRepository>();
    }
}