using Microsoft.AspNetCore.Identity;
using Users.Shared.Models;

namespace Users.Shared.Managers.Create;

public interface IUserCreationResult
{
    IEnumerable<IdentityError>? Errors { get; }
    string? UserId { get; }
    string? Code { get; }
    bool RequireConfirmedAccount { get; set; }
}