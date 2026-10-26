using Authentication.Application.Repositiories.ExternalLogin;
using Authentication.Application.ExternalLogin.Providers;
using Authentication.Domain.ExternalLogin;
using Authentication.Shared.ExternalLogin.Entities;
using Authentication.Shared.ExternalLogin.Managers;
using Microsoft.AspNetCore.Authentication;

namespace Authentication.Application.ExternalLogin.Managers;

internal class ExternalLoginInfoGetter : IExternalLoginInfoGetter
{
    private readonly IExternalLoginProvidersReadRepository _readRepository;
    private readonly IAuthenticationSchemeProvider _schemeProvider;
    private readonly IExternalLoginProviderResolver _providerResolver;

    public ExternalLoginInfoGetter(IExternalLoginProvidersReadRepository readRepository,
        IAuthenticationSchemeProvider schemeProvider, IExternalLoginProviderResolver providerResolver)
    {
        _readRepository = readRepository ?? throw new ArgumentNullException(nameof(readRepository));
        _schemeProvider = schemeProvider ?? throw new ArgumentNullException(nameof(schemeProvider));
        _providerResolver = providerResolver ?? throw new ArgumentNullException(nameof(providerResolver));
    }

    public async Task<IEnumerable<IExternalLoginMetadata>> GetAvailableExternalLoginProvidersAsync()
    {
        var providers = (await _readRepository.GetExternalLoginProvidersMetadata(true)).Where(p => p.IsEnabled);
        var schemes = await _schemeProvider.GetAllSchemesAsync();
        return providers.SelectMany(provider => schemes
            .Where(scheme => scheme.DisplayName is not null &&
                             _providerResolver.Resolve(scheme.Name) == provider.ProviderType)
            .Select(scheme => new ExternalLoginMetadata
            {
                ProviderType = provider.ProviderType,
                Provider = scheme.Name
            })).ToArray();
    }
}
