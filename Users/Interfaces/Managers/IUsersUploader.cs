using Abstract.Users;

namespace Users.Interfaces.Managers;

public interface IUsersUploader
{
    Task<IUserCreationResult> RegisterUser(IUser user);
}