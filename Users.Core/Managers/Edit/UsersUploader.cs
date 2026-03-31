using Users.Core.Repositories.Edit;
using Users.Shared.Managers.Create;
using Users.Shared.Managers.Edit;
using Users.Shared.Models;

namespace Users.Core.Managers.Edit;

internal class UsersUploader : IUsersUploader
{
    private IEditUsersRepository _editUsersRepository;

    public UsersUploader(IEditUsersRepository editUsersRepository)
    {
        _editUsersRepository = editUsersRepository ?? throw new ArgumentNullException(nameof(editUsersRepository));
    }

   public async Task<IUserCreationResult> RegisterUser(IUser user)
   {
       return await _editUsersRepository.CreateUser(user);
   }

   public async Task<bool> EditUserAsync(IUser user) 
       => await _editUsersRepository.EditUserAsync(user);


}