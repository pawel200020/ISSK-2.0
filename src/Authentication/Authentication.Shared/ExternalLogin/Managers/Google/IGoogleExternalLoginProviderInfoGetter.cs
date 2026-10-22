namespace Authentication.Shared.ExternalLogin.Managers.Google;

public interface IGoogleExternalLoginProviderInfoGetter
{
    Task<IExternalLoginProviderMetadata?> GetGoogleExternalLoginProviderInfoAsync();
}