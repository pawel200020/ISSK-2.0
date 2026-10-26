using Authentication.Shared.ExternalLogin;

namespace PortalBlazor.Mappers.Mappings.ExternalLogin;

public class ExternalLoginProvider : IExternalLoginProvider
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public ProviderType ProviderType { get; set; }
    public bool IsEnabled { get; set; }
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public bool IsClientSecretConfigured { get; set; }
}
