using Users.Shared.Managers.Create;
using Users.Shared.Models;

namespace Users.Shared.Managers.Edit;

public interface IUsersCreator
{
    Task<IUserCreationResult> RegisterUser(IUser user);
    Task<IUserCreationResult> RegisterUser(IUser user, string returnUrl);

}