using Authentication.Application.ExternalLogin.Providers;
using Authentication.Domain.ExternalLogin;
using Authentication.Shared.ExternalLogin;
using Authentication.Shared.ExternalLogin.Entities;
using Microsoft.AspNetCore.Identity;
using Users.Shared.Managers.Read;
using Users.Shared.Models;

namespace Authentication.Application.ExternalLogin.Managers;

public class UserExternalLoginInfoGetter : IUserExternalLoginInfoGetter
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IExternalLoginProviderResolver _providerResolver;
    private readonly IUserExternalLoginsGetter _externalLoginsGetter;

    public UserExternalLoginInfoGetter(SignInManager<ApplicationUser> signInManager,
        IExternalLoginProviderResolver providerResolver, IUserExternalLoginsGetter externalLoginsGetter)
    {
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _providerResolver = providerResolver ?? throw new ArgumentNullException(nameof(providerResolver));
        _externalLoginsGetter = externalLoginsGetter ?? throw new ArgumentNullException(nameof(externalLoginsGetter));
    }

    public async Task<IUserExternalLoginMetadata?> GetCurrentUserExternalLoginInfoMetadata()
    {
        var result = await _signInManager.GetExternalLoginInfoAsync();
        return result == null
            ? null
            : new UserExternalLoginMetadata()
            {
                Provider = result.LoginProvider,
                ProviderKey = result.ProviderKey,
                UserName = result.Principal?.Identity?.Name,
                AccessToken = result.AuthenticationTokens?
                    .FirstOrDefault(token => token.Name == "access_token")?.Value,
                ProviderType = _providerResolver.Resolve(result.LoginProvider)
            };
    }

    public async Task<IEnumerable<IExternalLoginMetadata>> GetUserExternalLoginInfos(Guid userId)
    {
        var result = await _externalLoginsGetter.GetUserExternalLoginsAsync(userId);
        
        return result.Select(login =>
            new ExternalLoginMetadata()
            {
                Provider = login.LoginProvider,
                ProviderType = _providerResolver.Resolve(login.LoginProvider),
                ProviderKey = login.ProviderKey,
            });
    }
    
    public async Task<ExternalLoginInfo?> GetCurrentUserExternalLoginInfo()
    {
        return await _signInManager.GetExternalLoginInfoAsync();
    }
}