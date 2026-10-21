using Authentication.Shared.ExternalLogin;
using Authentication.Shared.ExternalLogin.Managers;
using Data;
using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using PortalBlazor.Components.Account;
using Users.Shared.Models;

namespace PortalBlazor.Extension;

internal static class PortalAuthenticationExtension
{
    public static IServiceCollection AddPortalAuthentication(this IServiceCollection services)
    {
        var configuration = new GoogleProviderConfiguration();
        services.AddSingleton(configuration);

        services.AddCascadingAuthenticationState();
        services.AddScoped<CurrentUserProvider>();
        services.AddScoped<IdentityRedirectProcessor>();
        services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddGoogleOpenIdConnect(options =>
            {
                // Options are created lazily, after initialization loads the database settings.
                options.ClientId = configuration.Provider?.ClientId;
                options.ClientSecret = configuration.Provider?.ClientSecret;
                options.Scope.Add("https://www.googleapis.com/auth/user.birthday.read");
                options.SaveTokens = true;
            })
            .AddIdentityCookies();

        services.Configure<IdentityOptions>(options =>
        {
            options.Lockout.AllowedForNewUsers = false;
        });

        services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        return services;
    }

    // Call after Build and before the application starts handling requests.
    public static async Task InitializePortalAuthenticationAsync(this WebApplication app)
    {
        var configuration = app.Services.GetRequiredService<GoogleProviderConfiguration>();

        await using (var scope = app.Services.CreateAsyncScope())
        {
            var providerInfoGetter = scope.ServiceProvider.GetRequiredService<IGoogleExternalLoginProviderInfoGetter>();
            configuration.Provider = await providerInfoGetter.GetGoogleExternalLoginProviderInfoAsync();
        }

        if (configuration.Provider is not { IsEnabled: true } ||
            string.IsNullOrWhiteSpace(configuration.Provider.ClientId) ||
            string.IsNullOrWhiteSpace(configuration.Provider.ClientSecret))
        {
            app.Services.GetRequiredService<IAuthenticationSchemeProvider>()
                .RemoveScheme(GoogleOpenIdConnectDefaults.AuthenticationScheme);
        }
    }

    private sealed class GoogleProviderConfiguration
    {
        public IExternalLoginProviderMetadata? Provider { get; set; }
    }
}
