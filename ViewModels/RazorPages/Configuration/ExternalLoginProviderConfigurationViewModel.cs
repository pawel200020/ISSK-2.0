namespace ViewModels.RazorPages.Configuration;

public class ExternalLoginProviderConfigurationViewModel
{
    public int ProviderType { get; set; }
    public string ProviderName { get; set; } = "";
    public string ClientId { get; set; } = "";
    public string ClientSecret { get; set; } = "";
    public bool IsEnabled { get; set; }
}