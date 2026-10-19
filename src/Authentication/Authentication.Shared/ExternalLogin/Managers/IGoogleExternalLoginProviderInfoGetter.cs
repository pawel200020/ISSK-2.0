namespace Authentication.Shared.ExternalLogin.Managers;

public interface IGoogleExternalLoginProviderInfoGetter
{
    Task<IExternalLoginProviderMetadata?> GetGoogleExternalLoginProviderInfoAsync();
}