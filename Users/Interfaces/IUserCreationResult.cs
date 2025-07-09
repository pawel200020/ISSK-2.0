using Microsoft.AspNetCore.Identity;
using Users.Models;

namespace Users.Interfaces;

public interface IUserCreationResult
{
    IEnumerable<IdentityError>? Errors { get; }
    string? UserId { get; }
    string? Code { get; }
    ApplicationUser CreatedUser { get; set; }
    bool RequireConfirmedAccount { get; set; }
}