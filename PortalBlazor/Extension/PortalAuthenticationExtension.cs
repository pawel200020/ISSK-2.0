using Microsoft.AspNetCore.Identity;

namespace PortalBlazor.Extension;

internal static class PortalAuthenticationExtension
{
    public static async Task AddPortalAuthenticationAsync(
        this IServiceCollection services,
        string connectionString)
    {
        // var googleProvider =
        //     await ExternalLoginProvidersStartupLoader.GetGoogleProviderConfiguration(services.BuildServiceProvider());
        //
        // var authentication = services.AddAuthentication(options =>
        // {
        //     options.DefaultScheme = IdentityConstants.ApplicationScheme;
        //     options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        // });
        //
        // if (googleProvider is not null && googleProvider.IsEnabled)
        // {
        //     authentication.AddGoogleOpenIdConnect(options =>
        //     {
        //         options.ClientId = googleProvider.ClientId;
        //         options.ClientSecret = googleProvider.ClientSecret;
        //         options.Scope.Add("https://www.googleapis.com/auth/user.birthday.read");
        //         options.SaveTokens = true;
        //     });
        // }
        //
        // authentication.AddIdentityCookies();
        return;
    }
}
