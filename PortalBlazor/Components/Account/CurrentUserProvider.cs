using Microsoft.AspNetCore.Identity;
using Users.Shared.Managers.Read;
using Users.Shared.Models;

namespace PortalBlazor.Components.Account;

internal sealed class CurrentUserProvider(
    ICurrentUserProvider currentUserProvider,
    IdentityRedirectProcessor redirectProcessor)
{
    public async Task<ApplicationUser> GetRequiredUserAsync(HttpContext context)
    {
        var user = await currentUserProvider.GetCurrentUser(context);
        if (user is null)
        {
            redirectProcessor.RedirectToWithStatus("Account/InvalidUser",
                $"Error: Unable to load user with ID '{context.User.Identity}'.", context);
        }

        return user;
    }
    
    public Guid GetRequiredUserGuid(HttpContext context)
    {
        var userGuid = currentUserProvider.GetCurrentUserGuid(context);
        if (userGuid is null)
        {
            redirectProcessor.RedirectToWithStatus("Account/InvalidUser",
                $"Error: Unable to load user with ID '{context.User.Identity}'.", context);
        }

        return userGuid.Value;
    }
}