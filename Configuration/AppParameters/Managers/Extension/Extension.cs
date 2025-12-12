using Abstract.Configuration.AppParameters.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Configuration.AppParameters.Managers.Extension;

public static class Extension
{
    public static IServiceCollection AddAppParameters(this IServiceCollection builder)
        => builder.AddScoped<IAppParameterSaver, AppParameterSaver>()
            .AddScoped<IAppParameterGetter, AppParameterGetter>();
}