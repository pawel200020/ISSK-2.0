using Users.Core.Repositories.Read;
using Users.Shared.Managers.Read;
using Users.Shared.Models;

namespace Users.Core.Managers.Read;

internal class UserMetadataGetter : IUserMetadataGetter
{
    private readonly IReadUsersRepository _usersReadRepository;

    public UserMetadataGetter(IReadUsersRepository usersReadRepository)
    {
        _usersReadRepository = usersReadRepository ?? throw new ArgumentNullException(nameof(usersReadRepository));
    }

    public async Task<ApplicationUserMetadata> GetUserMetadataAsync(Guid userId)
    {
        var user = await _usersReadRepository.GetUserById(userId);
        return new ApplicationUserMetadata(userId, user.Email!, user.FirstName, user.LastName, user.EmailConfirmed, $"{user.FirstName} {user.LastName}");
    }
}