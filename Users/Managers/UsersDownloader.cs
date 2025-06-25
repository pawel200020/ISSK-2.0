using Abstract.Users;
using BlazorBootstrap;
using Users.Filters;
using Users.Interfaces.Managers;
using Users.Interfaces.Repositories;

namespace Users.Managers;

internal class UsersDownloader : IUsersDownloader
{
    private IUsersRepository _usersRepository;

    public UsersDownloader(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
    }

    public async Task<IEnumerable<IUser>> GetUsersPaged(int page, int pageSize, string query,
        IEnumerable<FilterItem> filters)
        => await _usersRepository.GetUsersPagedWithFilters(page, pageSize, filters);
}