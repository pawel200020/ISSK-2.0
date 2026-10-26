using Microsoft.AspNetCore.Identity;
using Users.Core.Repositories.Read;
using Users.Shared.Managers;
using Users.Shared.Managers.Read;

namespace Users.Core.Managers.Read;

internal class UserExternalLoginsGetter : IUserExternalLoginsGetter
{
    private readonly IReadUsersRepository _usersReadRepository;

    public UserExternalLoginsGetter(IReadUsersRepository usersReadRepository)
    {
        _usersReadRepository = usersReadRepository ?? throw new ArgumentNullException(nameof(usersReadRepository));
    }

    public async Task<IEnumerable<UserLoginInfo>> GetUserExternalLoginsAsync(Guid userId)
    {
        return await _usersReadRepository.GetUserConfiguredExternalLogins(userId);
    }
}