using Authentication.Application.Encryption;
using Authentication.Application.Repositiories.ExternalLogin;
using Authentication.Shared.ExternalLogin;
using Authentication.Shared.ExternalLogin.Managers;
using Microsoft.Extensions.Configuration;

namespace Authentication.Application.ExternalLogin.Managers;

internal class ExternalLoginConfigurationUpdater : IExternalLoginConfigurationUpdater
{
    private readonly IExternalLoginProvidersWriteRepository _externalLoginProvidersWriteRepository;
    private readonly IEncryptionManager _encryptionManager;
    private readonly string _clientSecretEncryptionKey;

    public ExternalLoginConfigurationUpdater(
        IExternalLoginProvidersWriteRepository externalLoginProvidersWriteRepository,
        IEncryptionManager encryptionManager,
        IConfiguration configuration)
    {
        _externalLoginProvidersWriteRepository = externalLoginProvidersWriteRepository ?? throw new ArgumentNullException(nameof(externalLoginProvidersWriteRepository));
        _encryptionManager = encryptionManager ?? throw new ArgumentNullException(nameof(encryptionManager));
        _clientSecretEncryptionKey = configuration["Authentication:ClientSecretEncryptionKey"]
            ?? throw new InvalidOperationException("Authentication:ClientSecretEncryptionKey is not configured.");
    }

    public async Task<bool> UpdateExternalLoginProviderAsync(
        IEnumerable<IExternalLoginProvider> providers)
    {
        foreach (var provider in providers)
        {
            if (!string.IsNullOrWhiteSpace(provider.ClientSecret))
                provider.ClientSecret = _encryptionManager.Encrypt(provider.ClientSecret, _clientSecretEncryptionKey);

            await _externalLoginProvidersWriteRepository.UpdateExternalLoginProviderAsync(provider);
        }
        return true;
    }
}