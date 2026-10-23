using Authentication.Application.ExternalLogin.Providers;
using Authentication.Domain.ExternalLogin;
using Authentication.Shared.ExternalLogin;
using Authentication.Shared.ExternalLogin.Entities;
using Microsoft.AspNetCore.Identity;
using Users.Shared.Models;

namespace Authentication.Application.ExternalLogin.Managers;

public class ExternalLoginInfoGetter : IExternalLoginInfoGetter
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IExternalLoginProviderResolver _providerResolver;

    public ExternalLoginInfoGetter(SignInManager<ApplicationUser> signInManager, IExternalLoginProviderResolver providerResolver)
    {
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _providerResolver = providerResolver ?? throw new ArgumentNullException(nameof(providerResolver));
    }

    public async Task<IExternalLoginMetadata?> GetExternalLoginInfoMetadata()
    {
        var result = await _signInManager.GetExternalLoginInfoAsync();
        return result == null
            ? null
            : new ExternalLoginMetadata()
            {
                Provider = result.LoginProvider, 
                ProviderKey = result.ProviderKey,
                UserName = result.Principal?.Identity?.Name,
                AccessToken = result.AuthenticationTokens?
                    .FirstOrDefault(token => token.Name == "access_token")?.Value,
                ProviderType = _providerResolver.Resolve(result.LoginProvider)
            };
    }
    
    public async Task<ExternalLoginInfo?> GetExternalLoginInfo()
    {
         return await _signInManager.GetExternalLoginInfoAsync();
    }
}