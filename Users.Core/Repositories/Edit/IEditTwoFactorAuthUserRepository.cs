namespace Users.Core.Repositories.Edit;

public interface IEditTwoFactorAuthUserRepository
{
    Task<string> Disable2FaAuthentication(Guid userGuid);

    Task<string> Enable2FaAuthentication(Guid userGuid);

    Task<IEnumerable<string>?> GenerateUserRecoveryCodes(Guid userGuid);
    Task<string?> GetUserAuthenticatorKey(Guid userGuid);
}