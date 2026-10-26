using Microsoft.AspNetCore.Identity;

namespace Users.Shared.Managers.Read;

public interface IUserExternalLoginsGetter
{
    Task<IEnumerable<UserLoginInfo>> GetUserExternalLoginsAsync(Guid userId);
}