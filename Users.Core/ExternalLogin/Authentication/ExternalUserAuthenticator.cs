using Microsoft.AspNetCore.Identity;
using Users.Shared.Authentication;
using Users.Shared.ExternalLogin.Authenticator;
using Users.Shared.Models;

namespace Users.Core.ExternalLogin.Authentication;

internal class ExternalUserAuthenticator : IExternalUserAuthenticator
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public ExternalUserAuthenticator(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    public async Task<AuthenticationResult> AuthenticateAsync(string loginProvider, string providerKey)
    {
        var result = await _signInManager.ExternalLoginSignInAsync(
            loginProvider,
            providerKey,
            isPersistent: false,
            bypassTwoFactor: true);
        
        if(result.Succeeded)
            return AuthenticationResult.Success;

        if(result.IsLockedOut)
            return AuthenticationResult.LockedOut;

        if(result.IsNotAllowed)
            return AuthenticationResult.NotAllowed;

        if(result.RequiresTwoFactor)
            return AuthenticationResult.TwoFactorRequired;

        return AuthenticationResult.Failed;
    }
}