using Abstract.Users;
using Event_Saver.Components.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Users.Filters;
using Users.Interfaces;
using Users.Interfaces.Managers;
using Users.Interfaces.Repositories;
using Users.Managers;
using Users.Models;
using Users.Repositories;
using Users.Repositories.Filters;

namespace Users;

public static class Extensions
{
    public static IServiceCollection AddUsers(this IServiceCollection services) =>
        services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>()
            .AddScoped<FirstNameFilter>()
            .AddScoped<LastNameFilter>()
            .AddScoped<UserNameFilter>()
            .AddScoped<EmailFilter>()
            .AddScoped<BirthDateFilter>()
            .AddScoped<IFilterFactory,FilterFactory>()
            .AddScoped<IUsersRepository,UsersRepository>()
            .AddScoped<IUsersDownloader,UsersDownloader>()
            .AddScoped<IUsersUploader, UsersUploader>();
}