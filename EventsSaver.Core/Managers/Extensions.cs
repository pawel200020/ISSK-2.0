using EventsSaver.Core.Managers.Lines;
using EventsSaver.Core.Managers.Seasons;
using EventsSaver.Shared.Managers.Lines;
using EventsSaver.Shared.Managers.Seasons;
using Microsoft.Extensions.DependencyInjection;

namespace EventsSaver.Core.Managers;

internal static class Extensions
{
    public static IServiceCollection AddManagers(this IServiceCollection services) =>
        services.AddScoped<ISeasonsGetter, SeasonsGetter>()
        .AddScoped<ISeasonCreator, SeasonCreator>()
        .AddScoped<ISeasonRemover, SeasonRemover>()
        .AddScoped<ILinesGetter,LinesGetter>();
}