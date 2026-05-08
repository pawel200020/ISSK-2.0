using Microsoft.AspNetCore.Identity;

namespace Users.Shared.Models;

public class ApplicationUser : IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateOnly BirthDate { get; set; }
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    public required DateTime TsInsert { get; set; }
}