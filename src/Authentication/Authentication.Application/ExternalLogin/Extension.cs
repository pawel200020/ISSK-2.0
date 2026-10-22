using Authentication.Application.ExternalLogin.Managers;
using Authentication.Application.ExternalLogin.Managers.Google;
using Authentication.Application.ExternalLogin.Providers;
using Authentication.Application.ExternalLogin.UserProfile;
using Authentication.Shared.ExternalLogin;
using Authentication.Shared.ExternalLogin.Managers;
using Authentication.Shared.ExternalLogin.Managers.Google;
using Authentication.Shared.ExternalLogin.UserProfile;
using Authentication.Shared.ExternalLoginProvider.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Application.ExternalLogin;

public static class Extension
{
    public static IServiceCollection AddExternalLogin(
        this IServiceCollection builder)
        => builder.AddScoped<IExternalLoginConfigurationGetter, ExternalLoginConfigurationGetter>()
            .AddScoped<IExternalLoginConfigurationUpdater, ExternalLoginConfigurationUpdater>()
            .AddScoped<IGoogleExternalLoginProviderInfoGetter, GoogleExternalLoginProviderInfoGetter>()
            .AddScoped<IExternalLoginInfoGetter, ExternalLoginInfoGetter>()
            .AddSingleton<IExternalLoginProviderResolver, ExternalLoginProviderResolver>()
            .AddScoped<IExternalUserProfileGetterFactory, ExternalUserInfoGetterFactory>();
}