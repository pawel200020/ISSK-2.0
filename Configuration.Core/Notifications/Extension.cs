using Configuration.Shared.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace Configuration.Notifications;

internal static class Extension
{
    public static IServiceCollection AddNotificationsConfiguratuion(this IServiceCollection builder)
        => builder.AddScoped<IEmailConfigurationGetter, EmailConfigurationGetter>();
}