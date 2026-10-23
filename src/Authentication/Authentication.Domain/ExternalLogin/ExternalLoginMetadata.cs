using Authentication.Shared.ExternalLogin;
using Authentication.Shared.ExternalLogin.Entities;

namespace Authentication.Domain.ExternalLogin;

public class ExternalLoginMetadata : IExternalLoginMetadata
{
    public required string Provider { get; init; }
    public required string ProviderKey { get; init; }
    public string? UserName { get; init; }
    public string? AccessToken { get; init; }
    public ProviderType ProviderType { get; init; }
}