using Authentication.Domain.ExternalLogin;
using Authentication.Shared.ExternalLogin;

namespace Authentication.Application.Repositiories.ExternalLogin;

public interface IExternalLoginProvidersReadRepository
{
    Task<IEnumerable<ExternalLoginProvider>> GetExternalLoginProvidersMetadata(bool cleanSensitiveData);
    Task<ExternalLoginProviderMetadata?> GetGoogleExternalLoginProvidersMetadata();


}