using Authentication.Application.ExternalLogin;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Application;

public static class Extension
{
    public static IServiceCollection AddApplication(
        this IServiceCollection builder)
        => builder.AddExternalLogin();


}