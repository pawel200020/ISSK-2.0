using EventsSaver.Core.Repositories.Line;
using EventsSaver.Core.Repositories.Seasons;
using Microsoft.Extensions.DependencyInjection;

namespace EventsSaver.Core.Repositories;

internal static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services.AddScoped<ISeasonsReadRepository, SeasonsReadRepository>()
            .AddScoped<ISeasonsWriteRepository, SeasonsWriteRepository>()
            .AddScoped<ILinesReadRepository, LinesReadRepository>();
}