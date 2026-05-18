using EventsSaver.Shared.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace EventsSaver.Core.Managers;

internal static class Extensions
{
    public static IServiceCollection AddManagers(this IServiceCollection services) =>
        services.AddScoped<ISeasonsGetter, SeasonsGetter>()
        .AddScoped<ISeasonCreator, SeasonCreator>()
        .AddScoped<ISeasonRemover, SeasonRemover>();
}