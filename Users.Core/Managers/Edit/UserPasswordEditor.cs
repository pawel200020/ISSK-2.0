using Users.Core.Entities;
using Users.Core.Repositories.Edit;
using Users.Shared.Managers.Edit;
using Resources.PortalResources;
using Users.Core.Repositories.Read;

namespace Users.Core.Managers.Edit;

internal class UserPasswordEditor : IUserPasswordEditor
{
    private readonly IEditUsersRepository _editUsersRepository;
    private readonly IReadUsersRepository _readUsersRepository;

    public UserPasswordEditor(IEditUsersRepository editUsersRepository, IReadUsersRepository readUsersRepository)
    {
        _editUsersRepository = editUsersRepository ?? throw new ArgumentNullException(nameof(editUsersRepository));
        _readUsersRepository = readUsersRepository ?? throw new ArgumentNullException(nameof(readUsersRepository));
    }
    
    public async Task<IEnumerable<string>> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
    {
        var result = await _editUsersRepository.ChangePasswordAsync(userId, oldPassword, newPassword);
        if (result.IsSuccess)
            return [];

        return GetErrorMessages(result.Messages);
    }
    
    public async Task<IEnumerable<string>> ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _readUsersRepository.TryGetUserByEmail(email);
        if (user == null)
            return [];
        
        var result = await _editUsersRepository.ResetPasswordAsync(new Guid(user.Id), token, newPassword);
        if (result.IsSuccess)
            return [];

        return GetErrorMessages(result.Messages);
    }
    private IEnumerable<string> GetErrorMessages(IEnumerable<Error?>? errors)
    {
        var messages = new List<string>();
        if (errors == null || !errors.Any()) return messages;

        foreach (var error in errors)
        {
            var msg = error.Code switch
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
            };
            messages.Add(string.IsNullOrWhiteSpace(msg) ? error.Description : msg ?? "");
        }

        return messages;
    }
}