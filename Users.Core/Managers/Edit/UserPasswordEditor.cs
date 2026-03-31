using Users.Core.Entities;
using Users.Core.Repositories.Edit;
using Users.Shared.Managers.Edit;
using Resources.PortalResources;

namespace Users.Core.Managers.Edit;

internal class UserPasswordEditor : IUserPasswordEditor
{
    private readonly IEditUsersRepository _editUsersRepository;

    public UserPasswordEditor(IEditUsersRepository editUsersRepository)
    {
        _editUsersRepository = editUsersRepository;
    }
    
    public async Task<IEnumerable<string>> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
    {
        var result = await _editUsersRepository.ChangePasswordAsync(userId, oldPassword, newPassword);
        if (result.IsSuccess)
            return [];

        var messages = new List<string>();
        if ( result.Messages == null || !result.Messages.Any()) return messages;

        messages.AddRange(from err in result.Messages
            let msg = err.Code switch
            {
                ErrorReason.UserNotFound => PortalResources.cUserNotFound,
                ErrorReason.WrongPassword => PortalResources.cInvalidOldPassword,
                ErrorReason.UserAlreadyExists => PortalResources.cUserAlreadyExists,
                ErrorReason.InvalidEmail => PortalResources.cInvalidEmail,
                ErrorReason.PasswordTooShort => PortalResources.cPasswordTooShort,
                ErrorReason.PasswordRequiresNonAlphanumeric => PortalResources.cPasswordRequiresNonAlphanumeric,
                ErrorReason.PasswordRequiresDigit => PortalResources.cPasswordRequiresDigit,
                ErrorReason.PasswordRequiresLower => PortalResources.cPasswordRequiresLower,
                ErrorReason.PasswordRequiresUpper => PortalResources.cPasswordRequiresUpper,
                ErrorReason.PasswordRequiresUniqueChars => PortalResources.cPasswordRequiresUniqueChars,
                _ => PortalResources.cUnknownError
            }
            select string.IsNullOrWhiteSpace(err.Description) ? msg : err.Description);

        return messages;
    }
}