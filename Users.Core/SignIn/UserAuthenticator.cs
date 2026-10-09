using Microsoft.AspNetCore.Identity;
using Users.Core.Repositories.Read;
using Users.Shared.Models;
using Users.Shared.SignIn;

namespace Users.Core.SignIn;

internal class UserAuthenticator : IUserAuthenticator
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IReadUsersRepository _usersRepository;

    public UserAuthenticator(SignInManager<ApplicationUser> signInManager, IReadUsersRepository usersRepository)
    {
        _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }
    
    public async Task<SignInStatus> AuthenticateAsync(ApplicationUser user, string password, bool rememberMe)
    {
        var result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
            return SignInStatus.Success;
        if (result.IsLockedOut)
            return SignInStatus.LockedOut;
        if (result.RequiresTwoFactor)
            return SignInStatus.RequiresTwoFactor;
        return SignInStatus.Failed;
    }
    
    public async Task ExternalAuthenticateAsync(ApplicationUser user, string loginProvider)
    {
        await _signInManager.SignInAsync(user, isPersistent: false, loginProvider);
    }
    
    public async Task RefreshAsync(Guid userId)
    {
        var currentUser = await _usersRepository.GetUserById(userId);
        await _signInManager.RefreshSignInAsync(currentUser);
    }
    
}