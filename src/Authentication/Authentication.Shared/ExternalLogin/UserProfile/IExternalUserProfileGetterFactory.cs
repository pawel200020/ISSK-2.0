using Authentication.Shared.ExternalLogin.UserProfile.Readers;

namespace Authentication.Shared.ExternalLogin.UserProfile;

public interface IExternalUserProfileGetterFactory
{
    IUserProfileGetter GetUserProfileGetter(ProviderType providerType);
}