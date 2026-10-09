using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Users.Shared.Models;

namespace Users.Core.ExternalServices.Google;

public class GoogleExternalLoginProvider
{
    private SignInManager<ApplicationUser> _signInManager;
    private ILogger<GoogleExternalLoginProvider> _logger;

    public GoogleExternalLoginProvider(SignInManager<ApplicationUser> signInManager, ILogger<GoogleExternalLoginProvider> logger)
    {
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task TrySignInWithGoogleAsync(string accessToken)
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            string errorMessage = "External login information is not available.";
            _logger.LogWarning(errorMessage);
            throw new InvalidOperationException(errorMessage);
        }

        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException("External login sign-in failed.");
        }
    }
}