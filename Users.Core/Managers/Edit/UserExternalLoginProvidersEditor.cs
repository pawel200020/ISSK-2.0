using Authentication.Shared.ExternalLogin;
using Microsoft.AspNetCore.Identity;
using Users.Core.Repositories.Edit;
using Users.Shared.Managers.Edit;
using Users.Shared.Models;

namespace Users.Core.Managers.Edit;

internal class UserExternalLoginProvidersEditor : IUserExternalLoginProvidersEditor
{
    private readonly IEditUsersRepository _editRepo;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserExternalLoginInfoGetter _externalLoginInfoGetter;

    public UserExternalLoginProvidersEditor(IEditUsersRepository editRepo, SignInManager<ApplicationUser> signInManager, IUserExternalLoginInfoGetter externalLoginInfoGetter)
    {
        _editRepo = editRepo ?? throw new ArgumentNullException(nameof(editRepo));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _externalLoginInfoGetter = externalLoginInfoGetter ?? throw new ArgumentNullException(nameof(externalLoginInfoGetter));
    }

    public async Task<bool> RemoveAssignedExternalLogin(Guid userId, string provider, string providerKey)
    {
        return await _editRepo.RemoveAssignedExternalLogin(userId, provider, providerKey);
    }

    public async Task<(bool,bool)> AddAssignedExternalLogin(Guid userId)
    {
        var userExternalLoginInfo = await _externalLoginInfoGetter.GetCurrentUserExternalLoginInfo();
        if (userExternalLoginInfo == null)
            return (false, false);
        var result = await _editRepo.AssignExternalProviderToUserAsync(userId, userExternalLoginInfo);
        return (true, result);
    }
}