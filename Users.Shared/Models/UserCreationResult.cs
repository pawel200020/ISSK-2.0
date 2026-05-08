using Microsoft.AspNetCore.Identity;
using Users.Shared.Managers.Create;

namespace Users.Shared.Models;

public class UserCreationResult : IUserCreationResult
{
    public IEnumerable<IdentityError>? Errors { get; init; }
    public string? UserId { get; set; }
    public string? Code { get; set; }
    public ApplicationUser CreatedUser { get; set; }
    public bool RequireConfirmedAccount { get; set; }
}