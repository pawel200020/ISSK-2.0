using Authentication.Shared.ExternalLogin.Entities;

namespace Authentication.Shared.ExternalLogin.Managers;

public interface IExternalLoginInfoGetter
{
    Task<IEnumerable<IExternalLoginMetadata>> GetAvailableExternalLoginProvidersAsync();
}