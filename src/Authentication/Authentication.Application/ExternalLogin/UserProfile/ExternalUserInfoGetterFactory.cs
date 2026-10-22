using Authentication.Application.ExternalLogin.UserProfile.Readers;
using Authentication.Shared.ExternalLogin;
using Authentication.Shared.ExternalLogin.UserProfile;
using Authentication.Shared.ExternalLogin.UserProfile.Readers;
using Microsoft.Extensions.Logging;

namespace Authentication.Application.ExternalLogin.UserProfile;

public class ExternalUserInfoGetterFactory : IExternalUserProfileGetterFactory
{
    private readonly ILogger<ExternalUserInfoGetterFactory> _logger;

    public ExternalUserInfoGetterFactory(ILogger<ExternalUserInfoGetterFactory> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IUserProfileGetter GetUserProfileGetter(ProviderType providerType)
    {
        switch (providerType)
        {
            case ProviderType.Google:
                return new GoogleUserProfileGetter(_logger);
            default:
                throw new InvalidOperationException($"Unknown provider type: {providerType}");
        }
    }
}