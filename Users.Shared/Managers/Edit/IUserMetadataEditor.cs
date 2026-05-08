namespace Users.Shared.Managers.Edit;

public interface IUserMetadataEditor
{
    Task<bool> ChangeUserEmail(Guid userId, string email, string token);
    Task<string> Disable2FaAuthentication(Guid userId);
    Task<string> Enable2FaAuthentication(Guid userId, string verificationCode);
    Task<IEnumerable<string>?> GenerateUserRecoveryCodes(Guid userId);
    Task<string?> GetUserAuthenticatorKey(Guid userId);
    Task SendConfirmationLink(string email);
}