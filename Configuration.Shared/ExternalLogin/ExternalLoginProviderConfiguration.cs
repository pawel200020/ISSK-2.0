namespace Configuration.Shared.ExternalLogin;

public class ExternalLoginProviderConfiguration : IExternalLoginProvider
{
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public bool IsEnabled { get; set; }
    public string ProviderName { get; set; } = string.Empty;
}
