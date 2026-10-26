using Authentication.Shared.ExternalLogin.Entities;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Shared.ExternalLogin;

public interface IUserExternalLoginInfoGetter
{
    Task<IUserExternalLoginMetadata?> GetCurrentUserExternalLoginInfoMetadata();
    Task<IEnumerable<IExternalLoginMetadata>> GetUserExternalLoginInfos(Guid userId);
    Task<ExternalLoginInfo?> GetCurrentUserExternalLoginInfo();

}