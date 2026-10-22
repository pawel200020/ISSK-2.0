using Authentication.Shared.ExternalLogin;

namespace Authentication.Application.ExternalLogin.Providers;

public class ExternalLoginProviderResolver : IExternalLoginProviderResolver
{
    private const string GoogleOpenIdConnectProviderName = "GoogleOpenIdConnect";

    public ProviderType Resolve(string providerName)
    {
        if(string.Equals(providerName, GoogleOpenIdConnectProviderName, StringComparison.OrdinalIgnoreCase))
        {
            return ProviderType.Google;
        }

        return ProviderType.Unknown;
    }
}