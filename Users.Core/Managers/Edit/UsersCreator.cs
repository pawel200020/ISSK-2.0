using Authentication.Shared.ExternalLogin;
using Users.Core.Repositories.Edit;
using Users.Shared.Managers;
using Users.Shared.Managers.Create;
using Users.Shared.Managers.Edit;
using Users.Shared.Models;

namespace Users.Core.Managers.Edit;

internal class UsersCreator : IUsersCreator
{
    private readonly IEditUsersRepository _editUsersRepository;
    private readonly IUserEmailConfirmation _userEmailConfirmation;
    private readonly IExternalLoginInfoGetter _externalLoginInfoGetter;
    

    public UsersCreator(IEditUsersRepository editUsersRepository, IUserEmailConfirmation userEmailConfirmation, IExternalLoginInfoGetter externalLoginInfoGetter)
    {
        _editUsersRepository = editUsersRepository ?? throw new ArgumentNullException(nameof(editUsersRepository));
        _userEmailConfirmation = userEmailConfirmation ?? throw new ArgumentNullException(nameof(userEmailConfirmation));
        _externalLoginInfoGetter = externalLoginInfoGetter ?? throw new ArgumentNullException(nameof(externalLoginInfoGetter));
    }
    
    public async Task<IUserCreationResult> RegisterUser(IUser user, string? returnUrl)
   {
       var createdUser =  await _editUsersRepository.CreateUser(user, false);
       if(createdUser.Errors != null && createdUser.Errors.Any())
           return createdUser;
       await _userEmailConfirmation.SendConfirmEmailWithReturnUrl(new Guid(createdUser.UserId), user.Email,createdUser.Code!, returnUrl);
       return createdUser;
   }
    
    public async Task<IUserCreationResult> RegisterExternalUser(IUser user, string? returnUrl)
    {
        var createdUser =  await _editUsersRepository.CreateUser(user, true);
        if(createdUser.Errors != null && createdUser.Errors.Any())
            return createdUser;
        
        var externalLoginInfo = await _externalLoginInfoGetter.GetExternalLoginInfo();
        if (externalLoginInfo == null)
            throw new InvalidOperationException("External login info not found");

        var userId = new Guid(createdUser.UserId!);
        await _editUsersRepository.AssignExternalProviderToUserAsync(userId, externalLoginInfo);
        await _userEmailConfirmation.SendConfirmEmailWithReturnUrl(userId, user.Email,createdUser.Code!, returnUrl);
        return createdUser;
    }
}