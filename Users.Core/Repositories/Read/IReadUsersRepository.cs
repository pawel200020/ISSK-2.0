using BlazorBootstrap;
using Users.Shared.Models;
using Users.Shared.Models.Roles;

namespace Users.Core.Repositories.Read;

internal interface IReadUsersRepository
{
    Task<ApplicationUser?> TryGetUserById(Guid id);
    Task<ApplicationUser> GetUserById(Guid userId);
    Task<ApplicationUser?> TryGetUserByEmail(string email);
    Task<bool> CheckUserPassword(Guid userGuid, string password);
    Task<bool> HasUser2FaEnabled(Guid userGuid);
    IUsersPaginatedList GetUsersPagedWithFilters(int page, int pageSize,
        IEnumerable<FilterItem> filters);
    Task<IEnumerable<ApplicationUser>?> SearchUsers(string searchPhrase, IEnumerable<UserRole> roles,
        int maxResultCount);
}