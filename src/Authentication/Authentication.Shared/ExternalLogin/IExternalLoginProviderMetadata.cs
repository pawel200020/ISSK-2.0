namespace Authentication.Shared.ExternalLogin;

public interface IExternalLoginProviderMetadata
{
    string ClientId { get; }
    string ClientSecret { get; }
    bool IsEnabled { get; }
}