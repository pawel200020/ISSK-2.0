using Abstract.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Users.Interfaces;
using Users.Interfaces.Repositories;
using Users.Models;
using FilterItem = BlazorBootstrap.FilterItem;

namespace Users.Repositories;

internal class UsersRepository : IUsersRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserStore<ApplicationUser> _userStore;

    private IFilterFactory _filterFactory;
   private readonly ILogger<UsersRepository> _logger;

    public UsersRepository(UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore, IFilterFactory filterFactory, ILogger<UsersRepository> logger)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
        _filterFactory = filterFactory;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task<IUserCreationResult> RegisterUser(IUser user)
    {
        var dbUser = CreateDbUser(user);
        
        await _userStore.SetUserNameAsync(dbUser, user.UserName, CancellationToken.None);
        var emailStore = GetEmailStore();
        await emailStore.SetEmailAsync(dbUser, user.Email, CancellationToken.None);
        var result = await _userManager.CreateAsync(dbUser, user.Password);
        
        if (!result.Succeeded)
            return new UserCreationResult() { Errors = result.Errors };
        
        await _userManager.AddToRoleAsync(dbUser, Enum.GetName(UserRolesWithGuids.RolesWithGuids[user.RoleId])!);

        _logger.LogInformation("User created a new account with password.");

        var userId = await _userManager.GetUserIdAsync(dbUser);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(dbUser);
        return new UserCreationResult() { UserId = userId, Code = code, CreatedUser = dbUser, RequireConfirmedAccount = _userManager.Options.SignIn.RequireConfirmedAccount};
    }

    private ApplicationUser CreateDbUser(IUser user)
    {
        try
        {
            var dbUser =  Activator.CreateInstance<ApplicationUser>();
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
    
    public async Task<IUsersPaginatedList> GetUsersPagedWithFilters(int page, int pageSize,
        IEnumerable<FilterItem> filters)
    {
        var filteredUsers = ApplyFiltering(filters);
        return new UsersPaginatedList()
        {
            Users = filteredUsers
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .Skip(page - 1)
                .Take(pageSize)
                .Select(user => new AppUser()
                {
                    Email = user.Email!,
                    Id = new Guid(user.Id),
                    IsAccountDisabled = user.LockoutEnabled,
                    PhoneNumber = user.PhoneNumber!,
                    UserName = user.UserName!,
                    Password = string.Empty,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate,
                    RoleId = new Guid(user.UserRoles.First().RoleId)
                }).ToArray(),
            TotalCount = GetTotalRowsNumber()
        };
    }

    public async Task<bool> EditUserAsync(IUser user)
    {
        var dbUser = await _userManager.Users.Include(x=>x.UserRoles).FirstOrDefaultAsync(x => x.Id == user.Id.ToString());
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

        UpdateDbUser(user,dbUser);
        await _userManager.UpdateAsync(dbUser);
      
        _logger.LogInformation("User successfully edited.");
        return true;
    }

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
        }

        return false;
    }
    
    private IUserEmailStore<ApplicationUser> GetEmailStore()
    {
        if (!_userManager.SupportsUserEmail)
            throw new NotSupportedException("The default UI requires a user store with email support.");

        return (IUserEmailStore<ApplicationUser>)_userStore;
    }

    private IQueryable<ApplicationUser> ApplyFiltering(IEnumerable<FilterItem> filters)
    {
        var users = _userManager.Users;
        foreach (var filter in filters)
        {
            var filterEvaluator = _filterFactory.Create(filter.PropertyName);
            users = filterEvaluator.Filter(users, filter);
        }

        return users;
    }

    private int GetTotalRowsNumber() => _userManager.Users.Count();
}