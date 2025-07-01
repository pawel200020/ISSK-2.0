using Microsoft.AspNetCore.Identity;

namespace Users.Models;

public class ApplicationRole : IdentityRole
{
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
}