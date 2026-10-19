using Authentication.Application;
using Authentication.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Api;

public static class Extension
{
    public static IServiceCollection AddAuthenticationModule(
        this IServiceCollection builder,
        string connectionString)
        =>
            builder.AddInfrastructure(connectionString)
                .AddApplication();
}