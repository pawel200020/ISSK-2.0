using Authentication.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace Authentication.Infrastructure;

public static class Extension
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<AuthenticationDbContext>(options =>
            options.UseSqlite(connectionString));
        services.AddRepositories();

        return services;
    }
}