using Users.Shared.Models;

namespace Users.Shared.Managers.Edit;

public interface IUsersEditor
{
    Task<bool> EditUserAsync(IUser user);
}