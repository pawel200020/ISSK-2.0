using Authentication.Shared.ExternalLogin;

namespace Authentication.Shared.ExternalLoginProvider.Managers;

public interface IExternalLoginConfigurationGetter
{
    Task<IEnumerable<IExternalLoginProvider>> GetAllProvidersWithoutSensitiveData();
}