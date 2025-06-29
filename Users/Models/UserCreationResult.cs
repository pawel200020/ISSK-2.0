using Abstract.Users;
using Microsoft.AspNetCore.Identity;
using Users.Interfaces;

namespace Users.Models;

public class UserCreationResult : IUserCreationResult
{
    public IEnumerable<IdentityError>? Errors { get; init; }
    public string? UserId { get; set; }
    public string? Code { get; set; }
    public ApplicationUser CreatedUser { get; set; }
}