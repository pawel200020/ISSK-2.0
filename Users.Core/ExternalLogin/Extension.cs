using Microsoft.Extensions.DependencyInjection;
using Users.Core.ExternalLogin.Authentication;
using Users.Shared.ExternalLogin.Authenticator;


namespace Users.Core.ExternalLogin;

public static class Extension
{
    public static IServiceCollection AddExternalLoginServices(this IServiceCollection services) =>
        services
            .AddScoped<IExternalUserAuthenticator,ExternalUserAuthenticator>();
}