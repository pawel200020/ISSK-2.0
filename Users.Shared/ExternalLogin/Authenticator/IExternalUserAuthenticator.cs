using Users.Shared.Authentication;

namespace Users.Shared.ExternalLogin.Authenticator;

public interface IExternalUserAuthenticator
{
    Task<AuthenticationResult> AuthenticateAsync(string loginProvider, string providerKey);
}