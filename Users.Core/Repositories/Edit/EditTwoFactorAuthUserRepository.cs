using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Users.Core.Repositories.Read;
using Users.Shared.Models;

namespace Users.Core.Repositories.Edit;

internal class EditTwoFactorAuthUserRepository : IEditTwoFactorAuthUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IReadUsersRepository _readUsersRepository;
    private readonly ILogger<EditTwoFactorAuthUserRepository> _logger;

    public EditTwoFactorAuthUserRepository(UserManager<ApplicationUser> userManager, IReadUsersRepository readUsersRepository)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _readUsersRepository = readUsersRepository ?? throw new ArgumentNullException(nameof(readUsersRepository));
    }

    public async Task<string> Disable2FaAuthentication(Guid userGuid) => await SetUser2FaEnable(userGuid, false);
    
    public async Task<string> Enable2FaAuthentication(Guid userGuid) => await SetUser2FaEnable(userGuid, true);

    private async Task<string> SetUser2FaEnable(Guid userGuid, bool enabled)
    {
        switch (enabled)
        {
            case true when await _readUsersRepository.HasUser2FaEnabled(userGuid):
                await Task.Yield();
                throw new InvalidOperationException($"User {userGuid} already has 2fa authentication enabled.");
            case false when !await _readUsersRepository.HasUser2FaEnabled(userGuid):
                await Task.Yield();
                throw new InvalidOperationException($"User {userGuid} does not have 2fa authentication.");
        }

        var user = await _readUsersRepository.GetUserById(userGuid);
        var result = await _userManager.SetTwoFactorEnabledAsync(user, enabled);
        if (!result.Succeeded)
        {
            var message = result.Errors.Select(e => e.Description);
            _logger.LogError(
                $"User {userGuid} disabled 2fa authentication with failure. Errors: {string.Join(", ", message)}");
            return string.Join(", ", message);
        }

        return "";
    }

    public async Task<IEnumerable<string>?> GenerateUserRecoveryCodes(Guid userGuid)
    {
        var user = await _readUsersRepository.GetUserById(userGuid);
        var recoveryCodes = await _userManager.CountRecoveryCodesAsync(user);
        if (recoveryCodes == 0)
            return await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
        
        return [];
    }
    
    public async Task<string?> GetUserAuthenticatorKey(Guid userGuid)
    {
        var user = await _readUsersRepository.GetUserById(userGuid);
        var key = await _userManager.GetAuthenticatorKeyAsync(user);
        
        if (string.IsNullOrEmpty(key))
        {
            await _userManager.ResetAuthenticatorKeyAsync(user);
            key = await _userManager.GetAuthenticatorKeyAsync(user);
        }

        return key;
    }
}