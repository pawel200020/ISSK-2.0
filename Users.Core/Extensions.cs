using Microsoft.Extensions.DependencyInjection;
using Users.Core.ExternalLogin;
using Users.Core.Filters;
using Users.Core.Filters.Factory;
using Users.Core.Managers;
using Users.Core.Managers.Edit;
using Users.Core.Managers.Read;
using Users.Core.Password;
using Users.Core.Repositories;
using Users.Core.Repositories.Edit;
using Users.Core.SignIn;
using Users.Shared.Managers.Edit;
using Users.Shared.Password;
using Users.Shared.SignIn;

namespace Users.Core;

public static class Extensions
{
    public static IServiceCollection AddUsers(this IServiceCollection services) =>
        services
            .AddScoped<FirstNameFilter>()
            .AddScoped<LastNameFilter>()
            .AddScoped<UserNameFilter>()
            .AddScoped<EmailFilter>()
            .AddScoped<BirthDateFilter>()
            .AddScoped<IFilterFactory, FilterFactory>()
            .AddScoped<IEditUsersRepository, EditUserRepository>()
            .AddScoped<IUsersCreator, UsersCreator>()
            .AddScoped<IUsersRemover, UsersRemover>()
            .AddScoped<IUserResetPasswordEmailSender, UserResetPasswordEmailSender>()
            .AddScoped<IUserAuthenticator, UserAuthenticator>()
            .AddScoped<IUserSearcher, UserSearcher>()
            .AddExternalLoginServices()
            .AddManagers()
            .AddRepositories();
}