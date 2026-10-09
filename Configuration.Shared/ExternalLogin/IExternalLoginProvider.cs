namespace Configuration.Shared.ExternalLogin;

public interface IExternalLoginProvider
{
    string ClientId { get; set; }
    string ClientSecret { get; set; }
    bool IsEnabled { get; set; }
    string ProviderName { get; set; }
}