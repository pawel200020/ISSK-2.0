using Authentication.Application.Repositiories.ExternalLogin;
using Authentication.Shared.ExternalLogin;
using Authentication.Shared.ExternalLogin.Managers;

namespace Authentication.Application.ExternalLogin.Managers;

public class ExternalLoginConfigurationGetter : IExternalLoginConfigurationGetter
{
    private readonly IExternalLoginProvidersReadRepository _externalLoginProvidersReadRepository;

    public ExternalLoginConfigurationGetter(IExternalLoginProvidersReadRepository externalLoginProvidersReadRepository)
    {
        _externalLoginProvidersReadRepository = externalLoginProvidersReadRepository ?? throw new ArgumentNullException(nameof(externalLoginProvidersReadRepository));
    }

    public async Task<IEnumerable<IExternalLoginProvider>> GetAllProvidersWithoutSensitiveData()
    {
        return await _externalLoginProvidersReadRepository.GetExternalLoginProvidersMetadata(true);
    }
}