using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Users.Core.Email;
using Users.Core.Filters;
using Users.Core.Filters.Factory;
using Users.Core.Managers;
using Users.Core.Managers.Edit;
using Users.Core.Managers.Read;
using Users.Core.Repositories;
using Users.Core.Repositories.Edit;
using Users.Shared;
using Users.Shared.Managers;
using Users.Shared.Managers.Edit;
using Users.Shared.Managers.Read;
using Users.Shared.Models;

namespace Users.Core;

public static class Extensions
{
    public static IServiceCollection AddUsers(this IServiceCollection services) =>
        services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>()
            .AddScoped<FirstNameFilter>()
            .AddScoped<LastNameFilter>()
            .AddScoped<UserNameFilter>()
            .AddScoped<EmailFilter>()
            .AddScoped<BirthDateFilter>()
            .AddScoped<IFilterFactory, FilterFactory>()
            .AddScoped<IEditUsersRepository, EditUserRepository>()
            .AddScoped<IUsersDownloader, UsersDownloader>()
            .AddScoped<IUsersUploader, UsersUploader>()
            .AddScoped<IUsersRemover, UsersRemover>()
            .AddManagers()
            .AddRepositories();
}