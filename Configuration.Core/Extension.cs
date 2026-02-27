using Configuration.AppParameters.Managers.Extension;
using Configuration.Encryption;
using Configuration.Managers;
using Configuration.Notifications;
using Configuration.Shared.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Configuration;

public static class Extension
{
    public static IServiceCollection AddConfiguration(this IServiceCollection builder)
        => builder.AddAppParameters()
            .AddNotificationsConfiguratuion()
            .AddScoped<IAppConfigurationGetter, AppConfigurationGetter>()
            .AddScoped<IAppConfigurationSaver, AppConfigurationSaver>()
            .AddSingleton<IEncryptionManager,EncryptionManager>();
}