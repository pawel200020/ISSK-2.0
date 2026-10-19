using Authentication.Shared.ExternalLoginProvider;

namespace Authentication.Shared.ExternalLogin.Managers;

public interface IExternalLoginConfigurationUpdater
{
    Task<bool> UpdateExternalLoginProviderAsync(
        IEnumerable<IExternalLoginProvider> providers);
}