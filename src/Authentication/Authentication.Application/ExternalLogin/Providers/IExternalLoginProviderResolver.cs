using Authentication.Shared.ExternalLogin;

namespace Authentication.Application.ExternalLogin.Providers;

public interface IExternalLoginProviderResolver
{
    ProviderType Resolve(string providerName);
}