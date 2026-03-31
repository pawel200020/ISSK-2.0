using Resources.PortalResources;
using Users.Core.Repositories.Edit;
using Users.Core.Repositories.Read;
using Users.Shared.Managers.Edit;

namespace Users.Core.Managers.Edit;

internal class UsersRemover : IUsersRemover
{
    private IEditUsersRepository _editUsersRepository;
    private IReadUsersRepository _readUsersRepository;

    public UsersRemover(IEditUsersRepository editUsersRepository, IReadUsersRepository readUsersRepository)
    {
        _editUsersRepository = editUsersRepository ?? throw new ArgumentNullException(nameof(editUsersRepository));
        _readUsersRepository = readUsersRepository ?? throw new ArgumentNullException(nameof(readUsersRepository));
    }

    public async Task<bool> DeleteUser(Guid userId) 
        => await _editUsersRepository.RemoveUserAsync(userId);

    public async Task<string> DeleteUserWithPasswordCheck(Guid userId, string inputPassword)
    {
        if (!await _readUsersRepository.CheckUserPassword(userId, inputPassword))
            return PortalResources.cInvalidPassword;

        await _editUsersRepository.RemoveUserAsync(userId);
        return "";
    }
}