using Abstract.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
   //private readonly ILogger _logger;

    public UsersRepository(UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore, IFilterFactory filterFactory)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
        _filterFactory = filterFactory;
        // _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task<IUserCreationResult> RegisterUser(IUser user, UserRole role)
    {
        var dbUser = CreateDbUser(user);
        
        await _userStore.SetUserNameAsync(dbUser, user.NickName, CancellationToken.None);
        var emailStore = GetEmailStore();
        await emailStore.SetEmailAsync(dbUser, user.Email, CancellationToken.None);
        var result = await _userManager.CreateAsync(dbUser, user.Password);
        
        if (!result.Succeeded)
            return new UserCreationResult() { Errors = result.Errors };

        await _userManager.AddToRoleAsync(dbUser, nameof(role));

        //Logger.LogInformation("User created a new account with password.");

        var userId = await _userManager.GetUserIdAsync(dbUser);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(dbUser);
        return new UserCreationResult() { UserId = userId, Code = code, CreatedUser = dbUser};
    }

    private ApplicationUser CreateDbUser(IUser user)
    {
        try
        {
            var dbUser =  Activator.CreateInstance<ApplicationUser>();
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.BirthDate = user.BirthDate;
            return dbUser;
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                                                $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor.");
        }
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
                    RoleId = user.UserRoles.First().RoleId
                }).ToArray(),
            TotalCount = GetTotalRowsNumber()
        };
    }

    public async Task<bool> EditUserAsync(IUser user)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
    
    
    private ApplicationUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<ApplicationUser>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                                                $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor.");
        }
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