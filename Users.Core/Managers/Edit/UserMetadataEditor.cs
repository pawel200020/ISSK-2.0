using System.Text;
using Microsoft.Extensions.Logging;
using Resources.PortalResources;
using Users.Core.Repositories.Edit;
using Users.Core.Repositories.Read;
using Users.Shared.Managers.Edit;

namespace Users.Core.Managers.Edit;

internal class UserMetadataEditor : IUserMetadataEditor
{
    private readonly IEditUsersRepository _editUsersRepository;
    private readonly IEditTwoFactorAuthUserRepository _editTwoFactorAuthUserRepository;
    private readonly ILogger<UserMetadataEditor> _logger;
    private readonly IReadTwoFactorAuthUserRepository _readTwoFactorAuthUserRepository;

    public UserMetadataEditor(IEditUsersRepository editUsersRepository, IEditTwoFactorAuthUserRepository twoFactorAuthUserRepository, ILogger<UserMetadataEditor> logger, IReadTwoFactorAuthUserRepository readTwoFactorAuthUserRepository)
    {
        _editUsersRepository = editUsersRepository ?? throw new ArgumentNullException(nameof(editUsersRepository));
        _editTwoFactorAuthUserRepository = twoFactorAuthUserRepository ?? throw new ArgumentNullException(nameof(twoFactorAuthUserRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _readTwoFactorAuthUserRepository = readTwoFactorAuthUserRepository ?? throw new ArgumentNullException(nameof(readTwoFactorAuthUserRepository));
    }

    public async Task<bool> ChangeUserEmail(Guid userId, string email, string token) 
        => await _editUsersRepository.ChangeEmailAsync(userId, email, token);

    public async Task<string> Disable2FaAuthentication(Guid userId)
        => await _editTwoFactorAuthUserRepository.Disable2FaAuthentication(userId);

    public async Task<string> Enable2FaAuthentication(Guid userId, string verificationCode)
    {
        verificationCode = verificationCode.Replace(" ", string.Empty).Replace("-", string.Empty);
        

        if (! await _readTwoFactorAuthUserRepository.VerifyTwoFactorTokenAsync(verificationCode, userId))
            return PortalResources.cInvalidVerificationCode;

        await _editTwoFactorAuthUserRepository.Enable2FaAuthentication(userId);
        _logger.LogInformation("User with ID '{UserId}' has enabled 2FA with an authenticator app.", userId);
        return "";
    } 
    
    public async Task<IEnumerable<string>?> GenerateUserRecoveryCodes(Guid userId)
        => await _editTwoFactorAuthUserRepository.GenerateUserRecoveryCodes(userId);

    public async Task<string?> GetUserAuthenticatorKey(Guid userId)
    {
        return await _editTwoFactorAuthUserRepository.GetUserAuthenticatorKey(userId);
    }
}