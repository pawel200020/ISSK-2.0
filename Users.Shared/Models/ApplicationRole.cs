using Microsoft.AspNetCore.Identity;

namespace Users.Shared.Models;

public class ApplicationRole : IdentityRole
{
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
}