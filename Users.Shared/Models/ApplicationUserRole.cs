using Microsoft.AspNetCore.Identity;

namespace Users.Shared.Models;

public class ApplicationUserRole : IdentityUserRole<string>
{
    public virtual  ApplicationUser User { get; set; }
    public virtual ApplicationRole Role { get; set; }
}