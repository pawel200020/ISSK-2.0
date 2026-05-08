using Users.Core.Repositories.Edit;
using Users.Core.Repositories.Read;
using Users.Shared.Managers.Edit;
using Users.Shared.Models;

namespace Users.Core.Managers.Edit;

internal class UsersEditor : IUsersEditor
{
    private readonly IEditUsersRepository _editUsersRepository;
    private readonly IReadUsersRepository _readUsersRepository;
    private readonly IUserMetadataEditor _userMetadataEditor;

    public UsersEditor(IEditUsersRepository editUsersRepository, IReadUsersRepository readUsersRepository,
        IUserMetadataEditor userMetadataEditor)
    {
        _editUsersRepository = editUsersRepository ?? throw new ArgumentNullException(nameof(editUsersRepository));
        _readUsersRepository = readUsersRepository ?? throw new ArgumentNullException(nameof(readUsersRepository));
        _userMetadataEditor = userMetadataEditor ?? throw new ArgumentNullException(nameof(userMetadataEditor));
    }

    public async Task<bool> EditUserAsync(IUser user)
    {
        var result = await _editUsersRepository.EditUserAsync(user);
        var updatedUser = await _readUsersRepository.GetUserById(user.Id);
        if (updatedUser is { EmailConfirmed: false, Email: not null })
            await _userMetadataEditor.SendConfirmationLink(updatedUser.Email);
        return result;
    }
}