using EventsSaver.Core.Managers;
using EventsSaver.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EventsSaver.Core;

public static class Extensions
{
    public static IServiceCollection AddEventSaver(this IServiceCollection services) =>
        services.AddRepositories()
            .AddManagers();
}