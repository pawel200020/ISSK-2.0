using Microsoft.Extensions.DependencyInjection;

namespace Notifications.Core.Email;

public static class Extension
{
    public static IServiceCollection AddEmail(this IServiceCollection builder) =>
        builder.AddSingleton<IEmailSenderFactory, EmailSenderFactory>();
}