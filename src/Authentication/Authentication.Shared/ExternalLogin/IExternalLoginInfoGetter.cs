using Authentication.Shared.ExternalLogin.Entities;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Shared.ExternalLogin;

public interface IExternalLoginInfoGetter
{
    Task<IExternalLoginMetadata?> GetExternalLoginInfoMetadata();
    Task<ExternalLoginInfo?> GetExternalLoginInfo();
}