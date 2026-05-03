namespace Users.Shared.Managers;

public interface IUserEmailConfirmation
{
    Task<bool> ConfirmEmailAsync(Guid userId, string token);
    Task<bool> SendConfirmationCurrentEmailAndGenerateToken(Guid userId);
    Task<bool> SendConfirmationNewEmailAndGenerateToken(Guid userId, string email);
    Task<bool> SendConfirmationEmailAccountWithReturnUrlAndGenerateToken(Guid userId, string email, string returnUrl);
    Task<bool> SendConfirmEmailWithReturnUrl(Guid userId, string email, string token, string returnUrl);
}