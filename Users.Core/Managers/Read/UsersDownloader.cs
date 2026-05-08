using BlazorBootstrap;
using Users.Core.Repositories.Read;
using Users.Shared.Managers.Read;
using Users.Shared.Models;

namespace Users.Core.Managers.Read;

internal class UsersDownloader : IUsersDownloader
{
    private readonly IReadUsersRepository _usersRepository;

    public UsersDownloader(IReadUsersRepository usersRepository)
    {
        _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
    }

    public Task<IUsersPaginatedList> GetUsersPaged(int page, int pageSize, string query,
        IEnumerable<FilterItem> filters)
        => Task.FromResult(_usersRepository.GetUsersPagedWithFilters(page, pageSize, filters));
    
    public async Task<ApplicationUser?> GetUserById(Guid id)
    => await _usersRepository.GetUserById(id);
}