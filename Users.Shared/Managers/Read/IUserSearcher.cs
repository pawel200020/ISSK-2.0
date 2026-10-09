using BlazorBootstrap;
using Users.Shared.Models;

namespace Users.Core.Managers.Read;

public interface IUserSearcher
{
    Task<IUsersPaginatedList> SearchAllUsersWithPagination (int page, int pageSize, IEnumerable<FilterItem> filters);
}