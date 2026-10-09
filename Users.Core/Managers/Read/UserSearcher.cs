using BlazorBootstrap;
using Users.Core.Repositories.Read;
using Users.Shared.Models;

namespace Users.Core.Managers.Read;

internal class UserSearcher : IUserSearcher
{
    private readonly IReadUsersRepository _usersRepository;

    public UserSearcher(IReadUsersRepository usersRepository)
    {
        _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
    }
    public Task<IUsersPaginatedList> SearchAllUsersWithPagination (int page, int pageSize, IEnumerable<FilterItem> filters)
        => Task.FromResult(_usersRepository.GetUsersPagedWithFilters(page, pageSize, filters));
    
}