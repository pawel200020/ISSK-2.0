using Users.Core.Repositories.Edit;
using Users.Shared.Managers;
using Users.Shared.Managers.Create;
using Users.Shared.Managers.Edit;
using Users.Shared.Models;

namespace Users.Core.Managers.Edit;

internal class UsersCreator : IUsersCreator
{
    private IEditUsersRepository _editUsersRepository;
    private IUserEmailConfirmation _userEmailConfirmation;

    public UsersCreator(IEditUsersRepository editUsersRepository, IUserEmailConfirmation userEmailConfirmation)
    {
        _editUsersRepository = editUsersRepository ?? throw new ArgumentNullException(nameof(editUsersRepository));
        _userEmailConfirmation = userEmailConfirmation ?? throw new ArgumentNullException(nameof(userEmailConfirmation));
    }
    
    public async Task<IUserCreationResult> RegisterUser(IUser user, string? returnUrl)
   {
       var createdUser =  await _editUsersRepository.CreateUser(user);
       if(createdUser.Errors != null && createdUser.Errors.Any())
           return createdUser;
       await _userEmailConfirmation.SendConfirmEmailWithReturnUrl(new Guid(createdUser.UserId), user.Email,createdUser.Code, returnUrl);
       return createdUser;
   }
}