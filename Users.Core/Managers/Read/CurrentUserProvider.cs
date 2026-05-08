using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Users.Shared.Managers.Read;
using Users.Shared.Models;

namespace Users.Core.Managers.Read;

public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly UserManager<ApplicationUser> _userManager;

    public CurrentUserProvider(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }
    
    public async Task<ApplicationUser?> GetCurrentUser(HttpContext context)
    {
        return await _userManager.GetUserAsync(context.User);
    }
    
    public Guid? GetCurrentUserGuid(HttpContext context)
    {
        var id = _userManager.GetUserId(context.User);
        if (string.IsNullOrWhiteSpace(id))
            return null;
        
        if (Guid.TryParse(id, out var guid))
            return guid;

        return null;
    }
}