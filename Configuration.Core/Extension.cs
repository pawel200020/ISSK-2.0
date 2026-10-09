using Configuration.AppParameters.Managers.Extension;
using Configuration.Culture;
using Configuration.Encryption;
using Configuration.ExternalLoginProviders;
using Configuration.Managers;
using Configuration.Notifications;
using Configuration.Repositories.Culture;
using Configuration.Repositories.ExternalLogin;
using Configuration.Shared.Culture;
using Configuration.Shared.ExternalLogin;
using Configuration.Shared.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Configuration;

public static class Extension
{
    public static IServiceCollection AddConfiguration(this IServiceCollection builder)
        => builder.AddAppParameters()
            .AddNotificationsConfiguratuion()
            .AddScoped<IExternalLoginProvidersRepository, ExternalLoginProvidersRepository>()
            .AddScoped<IAppConfigurationGetter, AppConfigurationGetter>()
            .AddScoped<IAppConfigurationSaver, AppConfigurationSaver>()
            .AddSingleton<IEncryptionManager, EncryptionManager>()
            .AddScoped<ISupportedLanguagesDownloader, SupportedLanguagesDownloader>()
            .AddScoped<ISupportedLanguagesRepository, SupportedLanguagesRepository>()
            .AddScoped<IExternalLoginProvidersGetter, ExternalLoginProvidersGetter>();
}