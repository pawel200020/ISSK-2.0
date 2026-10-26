namespace Authentication.Shared.ExternalLogin.Entities;

public interface IExternalLoginMetadata
{
    string Provider { get; }
    public string? ProviderKey { get; }
    ProviderType ProviderType { get; }
}