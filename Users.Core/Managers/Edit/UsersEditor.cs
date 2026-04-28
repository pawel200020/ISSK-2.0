using Users.Core.Repositories.Edit;
using Users.Shared.Models;

namespace Users.Core.Managers.Edit;

internal class UsersEditor
{
    private IEditUsersRepository _editUsersRepository;

    public UsersEditor(IEditUsersRepository editUsersRepository)
    {
        _editUsersRepository = editUsersRepository ?? throw new ArgumentNullException(nameof(editUsersRepository));
    }
    
    public async Task<bool> EditUserAsync(IUser user) 
        => await _editUsersRepository.EditUserAsync(user);
}