using Data.Configuration;
using Data.Languages;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Extension;

public static class Extension
{
    public static IServiceCollection AddData(this IServiceCollection builder) 
        => builder.AddScoped<ISupportedLanguagesRepository, SupportedLanguagesRepository>()
            .AddDbConfiguration();
}