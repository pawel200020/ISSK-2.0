namespace Authentication.Shared.ExternalLogin.Entities;

public interface IExternalLoginMetadata
{
    string Provider { get; }
    string ProviderKey { get; }
    string UserName { get; }
    string? AccessToken { get; }
    ProviderType ProviderType { get; }
}