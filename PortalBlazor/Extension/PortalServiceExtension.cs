using Configuration;
using Core.Extension;
using Data.Extension;

namespace PortalBlazor.Extension;

internal static class PortalServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection builder)
        => builder
            .AddData()
            .AddCore()
            .AddConfiguration();
}