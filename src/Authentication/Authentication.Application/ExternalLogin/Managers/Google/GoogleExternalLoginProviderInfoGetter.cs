using Authentication.Application.Encryption;
using Authentication.Application.Repositiories.ExternalLogin;
using Authentication.Shared.ExternalLogin;
using Authentication.Shared.ExternalLogin.Managers.Google;
using Microsoft.Extensions.Configuration;

namespace Authentication.Application.ExternalLogin.Managers.Google;

internal class GoogleExternalLoginProviderInfoGetter : IGoogleExternalLoginProviderInfoGetter
{
    private readonly IExternalLoginProvidersReadRepository _repository;
    private readonly IEncryptionManager _encryptionManager;
    private readonly string _clientSecretEncryptionKey;

    public GoogleExternalLoginProviderInfoGetter(
        IExternalLoginProvidersReadRepository repository,
        IEncryptionManager encryptionManager,
        IConfiguration configuration)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _encryptionManager = encryptionManager ?? throw new ArgumentNullException(nameof(encryptionManager));
        _clientSecretEncryptionKey = configuration["Authentication:ClientSecretEncryptionKey"]
            ?? throw new InvalidOperationException("Authentication:ClientSecretEncryptionKey is not configured.");
    }

    public async Task<IExternalLoginProviderMetadata?> GetGoogleExternalLoginProviderInfoAsync()
    {
        var metadata = await _repository.GetGoogleExternalLoginProvidersMetadata();
        if (metadata == null)
            return null;
        
        if (!string.IsNullOrWhiteSpace(metadata.ClientSecret))
            metadata.ClientSecret = _encryptionManager.Decrypt(metadata.ClientSecret, _clientSecretEncryptionKey);

        return metadata;
    }
}