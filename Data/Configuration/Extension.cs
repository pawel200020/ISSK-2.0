using Data.Configuration.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Configuration;

public static class Extension
{
    public static IServiceCollection AddDbConfiguration(this IServiceCollection builder)
        => builder.AddScoped<IAppParameterRepository, AppParameterRepository>()
            .AddScoped<IAppParameterEnumMapper, AppParameterEnumMapper>();
}