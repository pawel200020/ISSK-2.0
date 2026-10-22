namespace Authentication.Shared.ExternalLogin.Entities;

public interface IExternalLoginData
{
    string Provider { get; }
    string ProviderKey { get; }
    string UserName { get; }
    string? AccessToken { get; }
    ProviderType ProviderType { get; }
}