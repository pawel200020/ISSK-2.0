using Authentication.Api;
using Configuration;
using Data.Extension;
using EventsSaver.Core;
using Notifications.Core;

namespace PortalBlazor.Extension;

internal static class PortalServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection builder, string connectionString)
        => builder
            .AddData()
            .AddConfiguration()
            .AddNotifications()
            .AddEventSaver()
            .AddAuthenticationModule(connectionString);
}