namespace Users.Shared.SignIn;

public interface IUserAuthenticator
{

    Task ExternalAuthenticateAsync(Guid userId, string loginProvider);
    Task RefreshAsync(Guid userId);
}