using Users.Shared.Models;

namespace Users.Shared.SignIn;

public interface IUserAuthenticator
{
    Task<SignInStatus> AuthenticateAsync(ApplicationUser user, string password, bool rememberMe);
    Task RefreshAsync(Guid userId);
}