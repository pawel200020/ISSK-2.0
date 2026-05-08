using Users.Core.Repositories.Read;
using Users.Shared.Managers.Read;
using Users.Shared.Models;

namespace Users.Core.Managers.Read;

internal class UserMetadataGetter : IUserMetadataGetter
{
    private readonly IReadUsersRepository _editUsersRepository;

    public UserMetadataGetter(IReadUsersRepository editUsersRepository)
    {
        _editUsersRepository = editUsersRepository ?? throw new ArgumentNullException(nameof(editUsersRepository));
    }

    public async Task<ApplicationUserMetadata> GetUserMetadataAsync(Guid userId)
    {
        var user = await _editUsersRepository.GetUserById(userId);
        return new ApplicationUserMetadata(userId, user.Email!, user.FirstName, user.LastName, user.EmailConfirmed);
    }
}