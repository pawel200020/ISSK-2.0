using Microsoft.AspNetCore.Identity;
using Users.Core.Repositories.Read;
using Users.Shared.Models;

namespace Users.Core.Repositories.Edit;

internal class ReadTwoFactorAuthUserRepository : IReadTwoFactorAuthUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IReadUsersRepository _readUsersRepository;

    public ReadTwoFactorAuthUserRepository(UserManager<ApplicationUser> userManager, IReadUsersRepository readUsersRepository)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _readUsersRepository = readUsersRepository ?? throw new ArgumentNullException(nameof(readUsersRepository));
    }

    public async Task<bool> VerifyTwoFactorTokenAsync(string verificationCode, Guid userId)
    {
        var user = await _readUsersRepository.GetUserById(userId);
        return await _userManager.VerifyTwoFactorTokenAsync(
            user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);
    }
}