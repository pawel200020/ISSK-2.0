using Abstract.Users;
using BlazorBootstrap;

namespace Users.Interfaces.Managers;

public interface IUsersDownloader
{
    Task<IUsersPaginatedList> GetUsersPaged(int page, int pageSize, string query, IEnumerable<FilterItem> filters);
}