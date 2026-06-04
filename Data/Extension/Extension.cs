using Data.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Extension;

public static class Extension
{
    public static IServiceCollection AddData(this IServiceCollection builder) 
        => builder.AddDbConfiguration();
}