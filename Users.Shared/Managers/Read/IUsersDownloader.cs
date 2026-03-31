using BlazorBootstrap;
using Users.Shared.Models;

namespace Users.Shared.Managers.Read;

public interface IUsersDownloader
{
    Task<IUsersPaginatedList> GetUsersPaged(int page, int pageSize, string query, IEnumerable<FilterItem> filters);
    Task<ApplicationUser?> GetUserById(Guid id);
}