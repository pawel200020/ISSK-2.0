namespace Authentication.Shared.ExternalLogin.UserProfile.Readers;

public interface IUserProfileGetter
{
    Task<IExternalUserProfile?> GetProfileAsync(string authenticationProvider, string accessToken);
}