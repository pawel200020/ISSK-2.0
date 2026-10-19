using Authentication.Application.ExternalLogin.Managers;
using Authentication.Shared.ExternalLogin.Managers;
using Authentication.Shared.ExternalLoginProvider.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Application.ExternalLogin;

public static class Extension
{
    public static IServiceCollection AddExternalLogin(
        this IServiceCollection builder)
        => builder.AddScoped<IExternalLoginConfigurationGetter, ExternalLoginConfigurationGetter>()
            .AddScoped<IExternalLoginConfigurationUpdater, ExternalLoginConfigurationUpdater>()
            .AddScoped<IGoogleExternalLoginProviderInfoGetter, GoogleExternalLoginProviderInfoGetter>();
}