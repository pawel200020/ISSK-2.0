using System.Text.Json.Serialization;

namespace Configuration.Shared.ExternalLogin;

public class ExternalLoginProvidersConfiguration
{
    [JsonPropertyName("AuthenticationProviders")]
    public IEnumerable<IExternalLoginProvider> AuthenticationProviders { get; set; } = [];
}
