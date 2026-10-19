using Authentication.Application.Repositiories.ExternalLogin;
using Authentication.Shared.ExternalLogin;
using Authentication.Shared.ExternalLogin.Managers;

namespace Authentication.Application.ExternalLogin.Managers;

internal class GoogleExternalLoginProviderInfoGetter : IGoogleExternalLoginProviderInfoGetter
{
    private readonly IExternalLoginProvidersReadRepository _repository;

    public GoogleExternalLoginProviderInfoGetter(IExternalLoginProvidersReadRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IExternalLoginProviderMetadata?> GetGoogleExternalLoginProviderInfoAsync()
    {
        return (await _repository.GetGoogleExternalLoginProvidersMetadata());
    }
}