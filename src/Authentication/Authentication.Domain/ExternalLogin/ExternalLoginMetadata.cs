using Authentication.Shared.ExternalLogin;
using Authentication.Shared.ExternalLogin.Entities;

namespace Authentication.Domain.ExternalLogin;

public class ExternalLoginMetadata : IExternalLoginMetadata
{
    public string Provider { get; set; }
    public string? ProviderKey { get; set; }
    public ProviderType ProviderType { get; set; }
}