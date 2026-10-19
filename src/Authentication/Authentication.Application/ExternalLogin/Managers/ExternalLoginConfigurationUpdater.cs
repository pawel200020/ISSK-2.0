using Authentication.Application.Repositiories.ExternalLogin;
using Authentication.Shared.ExternalLogin;
using Authentication.Shared.ExternalLogin.Managers;
using Authentication.Shared.ExternalLoginProvider;

namespace Authentication.Application.ExternalLogin.Managers;

public class ExternalLoginConfigurationUpdater : IExternalLoginConfigurationUpdater
{
    private readonly IExternalLoginProvidersWriteRepository _externalLoginProvidersWriteRepository;

    public ExternalLoginConfigurationUpdater(IExternalLoginProvidersWriteRepository externalLoginProvidersWriteRepository)
    {
        _externalLoginProvidersWriteRepository = externalLoginProvidersWriteRepository ?? throw new ArgumentNullException(nameof(externalLoginProvidersWriteRepository));
    }

    public async Task<bool> UpdateExternalLoginProviderAsync(
        IEnumerable<IExternalLoginProvider> providers)
    {
        foreach (var provider in providers)
        {
            await _externalLoginProvidersWriteRepository.UpdateExternalLoginProviderAsync(provider);
        }
        return true;
    }   
}