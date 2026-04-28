using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resources.PortalResources;
using Users.Core.Entities;
using Users.Core.Repositories.Read;
using Users.Shared.Exceptions;
using Users.Shared.Managers.Create;
using Users.Shared.Models;

namespace Users.Core.Repositories.Edit;

internal class EditUserRepository : IEditUsersRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IReadUsersRepository _readUsersRepository;
    private readonly IUserStore<ApplicationUser> _userStore;
    private readonly IdentityErrorDescriber _errorDescriber;

    private readonly ILogger<EditUserRepository> _logger;

    public EditUserRepository(UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore,
        ILogger<EditUserRepository> logger, IdentityErrorDescriber errorDescriber,
        IReadUsersRepository readUsersRepository)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _errorDescriber = errorDescriber ?? throw new ArgumentNullException(nameof(errorDescriber));
        _readUsersRepository = readUsersRepository ?? throw new ArgumentNullException(nameof(readUsersRepository));
    }

    public async Task<IUserCreationResult> CreateUser(IUser user)
    {
        var dbUser = CreateDbUser(user);

        await _userStore.SetUserNameAsync(dbUser, user.UserName, CancellationToken.None);
        var emailStore = GetEmailStore();
        var userSameEmail = await _userManager.FindByEmailAsync(user.Email);
        var isUniqueEmail = userSameEmail == null;
        if (!isUniqueEmail)
        {
            return new UserCreationResult()
            {
                Errors = new List<IdentityError>()
                    { new() { Code = "", Description = PortalResources.cEmailAlreadyRegistered } }
            };
        }
        await emailStore.SetEmailAsync(dbUser, user.Email, CancellationToken.None);
        var result = await _userManager.CreateAsync(dbUser, user.Password);

        if (!result.Succeeded)
            return new UserCreationResult() { Errors = result.Errors };

        await _userManager.AddToRoleAsync(dbUser, Enum.GetName(UserRolesWithGuids.RolesWithGuids[user.RoleId])!);
        
        _logger.LogInformation("User created a new account with password.");

        var userId = await _userManager.GetUserIdAsync(dbUser);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(dbUser);
        return new UserCreationResult()
        {
            UserId = userId, Code = code, CreatedUser = dbUser,
            RequireConfirmedAccount = _userManager.Options.SignIn.RequireConfirmedAccount
        };
    }

    private ApplicationUser CreateDbUser(IUser user)
    {
        try
        {
            var dbUser = Activator.CreateInstance<ApplicationUser>();
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.BirthDate = user.BirthDate;
            dbUser.PhoneNumber = user.PhoneNumber;
            return dbUser;
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                                                $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor.");
        }
    }

    private void UpdateDbUser(IUser user, ApplicationUser dbUser)
    {
        dbUser.FirstName = user.FirstName;
        dbUser.LastName = user.LastName;
        dbUser.BirthDate = user.BirthDate;
        dbUser.UserName = user.UserName;
        dbUser.PhoneNumber = user.PhoneNumber;
    }

    public async Task<bool> EditUserAsync(IUser user)
    {
        var dbUser = await _userManager.Users.Include(x => x.UserRoles)
            .FirstOrDefaultAsync(x => x.Id == user.Id.ToString());
        if (dbUser is null)
            return false;

        await _userStore.SetUserNameAsync(dbUser, user.UserName, CancellationToken.None);
        var emailStore = GetEmailStore();
        await emailStore.SetEmailAsync(dbUser, user.Email, CancellationToken.None);

        if (dbUser.UserRoles.First().RoleId != user.RoleId.ToString())
        {
            await _userManager.RemoveFromRoleAsync(dbUser, dbUser.UserRoles.First().Role.ToString());
            await _userManager.AddToRoleAsync(dbUser, GetUserRoleNameByGuid(user.RoleId));
        }

        UpdateDbUser(user, dbUser);
        await _userManager.UpdateAsync(dbUser);

        _logger.LogInformation("User successfully edited.");
        return true;
    }

    public async Task<UserOperationResult> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            return new UserOperationResult()
                { IsSuccess = false, Messages = [new Error() { Code = ErrorReason.UserNotFound }] };

        var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        if (!result.Succeeded)
        {
            _logger.LogError(
                $"User {userId} changed their password with failure. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");

            // If any of the errors indicate the current password was incorrect, map to WrongPassword
            var pwdMismatchCode = _errorDescriber.PasswordMismatch().Code;
            if (result.Errors.Any(e => string.Equals(e.Code, pwdMismatchCode, StringComparison.OrdinalIgnoreCase)))
            {
                return new UserOperationResult()
                    { IsSuccess = false, Messages = [new Error() { Code = ErrorReason.WrongPassword }] };
            }

            var mapped = result.Errors.Select(e => new Error()
            {
                Code = e.Code switch
                {
                    var c when string.Equals(c,
                        _errorDescriber.PasswordTooShort(_userManager.Options.Password.RequiredLength).Code,
                        StringComparison.OrdinalIgnoreCase) => ErrorReason.PasswordTooShort,
                    var c when string.Equals(c, _errorDescriber.PasswordRequiresNonAlphanumeric().Code,
                        StringComparison.OrdinalIgnoreCase) => ErrorReason.PasswordRequiresNonAlphanumeric,
                    var c when string.Equals(c, _errorDescriber.PasswordRequiresDigit().Code,
                        StringComparison.OrdinalIgnoreCase) => ErrorReason.PasswordRequiresDigit,
                    var c when string.Equals(c, _errorDescriber.PasswordRequiresLower().Code,
                        StringComparison.OrdinalIgnoreCase) => ErrorReason.PasswordRequiresLower,
                    var c when string.Equals(c, _errorDescriber.PasswordRequiresUpper().Code,
                        StringComparison.OrdinalIgnoreCase) => ErrorReason.PasswordRequiresUpper,
                    var c when string.Equals(c,
                        _errorDescriber.PasswordRequiresUniqueChars(_userManager.Options.Password.RequiredUniqueChars)
                            .Code, StringComparison.OrdinalIgnoreCase) => ErrorReason.PasswordRequiresUniqueChars,
                    _ => ErrorReason.Unknown
                },
                Description = e.Description
            }).ToList();

            return new UserOperationResult() { IsSuccess = false, Messages = mapped };
        }

        _logger.LogInformation("User changed their password successfully.");
        return new UserOperationResult() { IsSuccess = true };
    }

    public async Task<bool> ChangeEmailAsync(Guid userId, string newEmail, string token)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            await Task.Yield();
            throw new UserNotFoundException();
        }

        await _userManager.ChangeEmailAsync(user, newEmail, token);
        return true;
    }

    public async Task<string> GenerateChangeEmailTokenAsync(Guid userId, string newEmail)
    {
        var user = await _readUsersRepository.GetUserById(userId);
        return await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
    }
    
    public async Task<string> GenerateEmailConfirmationTokenAsync(Guid userId)
    {
        var user = await _readUsersRepository.GetUserById(userId);
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<bool> ConfirmEmailAsync(ApplicationUser user, string token) =>
        (await _userManager.ConfirmEmailAsync(user, token)).Succeeded;


    private string GetUserRoleNameByGuid(Guid guid)
        => UserRolesWithGuids.RolesWithGuids[guid].ToString();

    public async Task<bool> RemoveUserAsync(Guid userId)
    {
        try
        {
            var userToRemove = _userManager.Users.Single(u => u.Id == userId.ToString());
            await _userManager.DeleteAsync(userToRemove);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            await Task.Yield();
            throw;
        }
    }

    private IUserEmailStore<ApplicationUser> GetEmailStore()
    {
        if (!_userManager.SupportsUserEmail)
            throw new NotSupportedException("The default UI requires a user store with email support.");

        return (IUserEmailStore<ApplicationUser>)_userStore;
    }
}