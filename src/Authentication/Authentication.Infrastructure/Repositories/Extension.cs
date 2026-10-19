using Authentication.Application.Repositiories;
using Authentication.Application.Repositiories.ExternalLogin;
using Authentication.Infrastructure.Repositories.ExternalLogin;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Infrastructure.Repositories;

public static class Extension
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services)
        => services.AddScoped<IExternalLoginProvidersReadRepository, ExternalLoginProvidersReadRepository>()
        .AddScoped<IExternalLoginProvidersWriteRepository, ExternalLoginProvidersWriteRepository>();
}