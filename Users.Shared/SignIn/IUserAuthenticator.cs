namespace Users.Shared.SignIn;

public interface IUserAuthenticator
{

    Task<SignInStatus> AuthenticateAsync(string userName, string password, bool rememberMe);
    Task AuthenticateAsync(Guid userId);
    Task ExternalAuthenticateAsync(Guid userId, string loginProvider);
    Task RefreshAsync(Guid userId);
}