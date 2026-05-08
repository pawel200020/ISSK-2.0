using Microsoft.Extensions.DependencyInjection;
using Notifications.Shared.Email;

namespace Notifications.Core.Email;

public static class Extension
{
    public static IServiceCollection AddEmail(this IServiceCollection builder) =>
        builder.AddScoped<IEmailSenderFactory, EmailSenderFactory>()
            .AddScoped<IEmailService, EmailService>();
}