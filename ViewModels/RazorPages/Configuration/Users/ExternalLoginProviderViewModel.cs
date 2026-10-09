namespace ViewModels.RazorPages.Configuration.Users;

public class ExternalLoginProviderViewModel
{
    public string Name { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = false;
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
}