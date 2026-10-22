using Authentication.Shared.ExternalLogin.Entities;

namespace Authentication.Shared.ExternalLogin;

public interface IExternalLoginInfoGetter
{
    Task<IExternalLoginData?> GetExternalLoginInfo();
}