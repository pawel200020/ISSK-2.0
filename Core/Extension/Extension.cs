using Abstract.Languages;
using Core.Languages;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extension;

public static class Extension
{
    public static IServiceCollection AddCore(this IServiceCollection builder)
        => builder.AddScoped<ISupportedLanguagesDownloader, SupportedLanguagesDownloader>();
}