using Microsoft.AspNetCore.Identity;
using Users.Shared.Models;

namespace Users.Core.ExternalServices;

public class ExternalLoginProvidersRepository
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    public ExternalLoginProvidersRepository(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }
    
    public async Task<IEnumerable<string>> GetAllSupportedExternalLoginProviders()
    {
        
        return (await _signInManager.GetExternalAuthenticationSchemesAsync()).Select(provider => provider.Name);
    }
}