using Microsoft.AspNetCore.Http;
using Users.Shared.Models;

namespace Users.Shared.Managers.Read;

public interface ICurrentUserProvider
{
    Task<ApplicationUser?> GetCurrentUser(HttpContext context);
    Guid? GetCurrentUserGuid(HttpContext context);
}