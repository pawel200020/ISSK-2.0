using Configuration.Shared.ExternalLogin;

namespace Configuration.ExternalLoginProviders;

internal class ExternalLoginProvider : IExternalLoginProvider
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public bool IsEnabled { get; set; }
    public string ProviderName { get; set; }
}