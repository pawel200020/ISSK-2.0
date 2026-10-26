namespace Authentication.Shared.ExternalLogin.Managers;

public interface IExternalLoginConfigurationGetter
{
    Task<IEnumerable<IExternalLoginProvider>> GetAllProvidersWithoutSensitiveData();
}