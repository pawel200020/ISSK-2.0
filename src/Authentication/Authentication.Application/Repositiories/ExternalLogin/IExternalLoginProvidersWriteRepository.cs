using Authentication.Shared.ExternalLogin;
using Authentication.Shared.ExternalLoginProvider;

namespace Authentication.Application.Repositiories.ExternalLogin;

public interface IExternalLoginProvidersWriteRepository
{
    Task UpdateExternalLoginProviderAsync(IExternalLoginProvider provider);
}