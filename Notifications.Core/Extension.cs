using Microsoft.Extensions.DependencyInjection;
using Notifications.Core.Email;

namespace Notifications.Core;

public static class Extension
{
    public static IServiceCollection AddNotifications(this IServiceCollection builder)=>
        builder.AddEmail();
}