using Microsoft.Extensions.DependencyInjection;
using Users.Core.Repositories.Edit;
using Users.Core.Repositories.Read;

namespace Users.Core.Repositories;

internal static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services.AddScoped<IEditUsersRepository, EditUserRepository>()
            .AddScoped<IReadUsersRepository, ReadUsersRepository>()
            .AddScoped<IEditTwoFactorAuthUserRepository,EditTwoFactorAuthUserRepository>()
            .AddScoped<IReadTwoFactorAuthUserRepository, ReadTwoFactorAuthUserRepository>();
}