using Abstract.Users;
using BlazorBootstrap;

namespace Users.Interfaces.Repositories;

internal interface IUsersRepository
{
    Task<IUserCreationResult> RegisterUser(IUser user);
    Task<IUsersPaginatedList> GetUsersPagedWithFilters(int page,int pageSize, IEnumerable<FilterItem> filters);
    Task<bool> EditUserAsync(IUser user);
    Task<bool> RemoveUserAsync(Guid userId);
}