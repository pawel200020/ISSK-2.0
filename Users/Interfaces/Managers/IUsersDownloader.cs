using Abstract.Users;
using BlazorBootstrap;

namespace Users.Interfaces.Managers;

public interface IUsersDownloader
{
    Task<IEnumerable<IUser>> GetUsersPaged(int page, int pageSize, string query, IEnumerable<FilterItem> filters);
}