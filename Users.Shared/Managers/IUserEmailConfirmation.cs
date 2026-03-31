using Users.Shared.Models;

namespace Users.Shared.Managers;

public interface IUserEmailConfirmation
{
    Task<bool> ConfirmEmailAsync(ApplicationUser user, string token);
    Task<bool> SendConfirmationCurrentEmail(Guid userId);
    Task<bool> SendConfirmationNewEmail(Guid userId, string email);
}