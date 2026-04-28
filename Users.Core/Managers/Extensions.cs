using Microsoft.Extensions.DependencyInjection;
using Users.Core.Email;
using Users.Core.Managers.Edit;
using Users.Core.Managers.Read;
using Users.Shared.Managers;
using Users.Shared.Managers.Edit;
using Users.Shared.Managers.Read;

namespace Users.Core.Managers;

internal static class Extensions
{
    public static IServiceCollection AddManagers(this IServiceCollection services) =>
        services.AddScoped<IUserEmailConfirmation, UserEmailConfirmation>()
            .AddScoped<IUserMetadataEditor, UserMetadataEditor>()
            .AddScoped<ICurrentUserProvider, CurrentUserProvider>()
            .AddScoped<IUserPasswordEditor, UserPasswordEditor>()
            .AddScoped<IUserMetadataGetter, UserMetadataGetter>();
}