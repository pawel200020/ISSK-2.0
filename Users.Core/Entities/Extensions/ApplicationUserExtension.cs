using Users.Shared.Models;

namespace Users.Core.Entities.Extensions;

public static class ApplicationUserExtension
{
    public static AppUser ToDomainUser(this ApplicationUser user)
    {
        return new AppUser
        {
            Id = Guid.Parse(user.Id),
            FirstName = user.FirstName,
            LastName = user.LastName,
            BirthDate = user.BirthDate,
            UserName = user.UserName!,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber!,
            IsEmailConfirmed = user.EmailConfirmed,
            IsAccountDisabled = user.LockoutEnabled && user.LockoutEnd > DateTime.UtcNow,
            RoleId = user.UserRoles.FirstOrDefault() is not null?  new Guid(user.UserRoles.FirstOrDefault()?.RoleId!) : Guid.Empty
        };
    }
}