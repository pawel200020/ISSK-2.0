using Authentication.Shared.ExternalLogin;

namespace Authentication.Domain.ExternalLoginProviders;

public class ExternalLoginProviderMetadata : IExternalLoginProviderMetadata
{
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public bool IsEnabled { get; set; }
}