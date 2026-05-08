using Users.Core.Entities;
using Users.Shared.Managers.Create;
using Users.Shared.Models;

namespace Users.Core.Repositories.Edit;

internal interface IEditUsersRepository
{
    Task<IUserCreationResult> CreateUser(IUser user);
    Task<bool> EditUserAsync(IUser user);
    Task<bool> RemoveUserAsync(Guid userId);
    Task<UserOperationResult> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);
    Task<bool> ChangeEmailAsync(Guid userId, string newEmail, string token);
    Task<string> GenerateChangeEmailTokenAsync(Guid userId, string newEmail);
    Task<bool> ConfirmEmailAsync(ApplicationUser user, string token);
    Task<string> GenerateEmailConfirmationTokenAsync(Guid userId);
    Task<string> GeneratePasswordResetTokenAsync(Guid userId);
    Task<UserOperationResult> ResetPasswordAsync(Guid userId, string token, string newPassword);

}