using Abstract.Users;
using BlazorBootstrap;

namespace Users.Interfaces.Repositories;

internal interface IUsersRepository
{
    Task<bool> AddUserAsync(IUser user, UserRole? role);
    Task<IUsersPaginatedList> GetUsersPagedWithFilters(int page,int pageSize, IEnumerable<FilterItem> filters);
    Task<bool> EditUserAsync(IUser user);
    Task<bool> RemoveUserAsync(Guid userId);
}