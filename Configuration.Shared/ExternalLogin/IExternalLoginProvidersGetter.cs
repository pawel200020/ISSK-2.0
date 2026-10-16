namespace Configuration.Shared.ExternalLogin;

public interface IExternalLoginProvidersGetter
{
    Task<IEnumerable<IExternalLoginProvider>> GetAllProvidersWithoutSensitiveData();
    Task<IExternalLoginProvider> GetGoogleProviderConfiguration();
}