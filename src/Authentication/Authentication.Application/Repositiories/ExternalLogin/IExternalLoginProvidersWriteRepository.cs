using Authentication.Shared.ExternalLogin;

namespace Authentication.Application.Repositiories.ExternalLogin;

public interface IExternalLoginProvidersWriteRepository
{
    Task UpdateExternalLoginProviderAsync(IExternalLoginProvider provider);
}
