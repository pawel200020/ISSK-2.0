using Users.Shared.Managers.Create;
using Users.Shared.Models;

namespace Users.Shared.Managers.Edit;

public interface IUsersUploader
{
    Task<IUserCreationResult> RegisterUser(IUser user);
    Task<bool> EditUserAsync(IUser user);
}