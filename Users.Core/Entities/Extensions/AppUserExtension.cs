using Users.Shared.Models;

namespace Users.Core.Entities.Extensions;

internal static class AppUserExtension
{
    public static ApplicationUserMetadata GetMetadata(this IUser user)
    {
        return new ApplicationUserMetadata(user.Id,user.UserName, user.Email, user.FirstName, user.LastName, user.IsEmailConfirmed, user.PhoneNumber,
            $"{user.FirstName} {user.LastName}");
    }
}