using Configuration;
using Data.Extension;
using EventsSaver.Core;
using Notifications.Core;

namespace PortalBlazor.Extension;

internal static class PortalServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection builder)
        => builder
            .AddData()
            .AddConfiguration()
            .AddNotifications()
            .AddEventSaver();
}