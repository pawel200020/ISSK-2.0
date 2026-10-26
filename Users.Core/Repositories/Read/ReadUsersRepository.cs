using Authentication.Shared.ExternalLogin.Entities;
using BlazorBootstrap;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Users.Core.Filters.Factory;
using Users.Shared.Exceptions;
using Users.Shared.Models;
using Users.Shared.Models.Roles;

namespace Users.Core.Repositories.Read;

internal class ReadUsersRepository : IReadUsersRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IFilterFactory _filterFactory;

    public ReadUsersRepository(UserManager<ApplicationUser> userManager, IFilterFactory filterFactory)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _filterFactory = filterFactory ?? throw new ArgumentNullException(nameof(filterFactory));
    }

    public async Task<ApplicationUser?> TryGetUserById(Guid id)
        => await _userManager.FindByIdAsync(id.ToString());

    public async Task<ApplicationUser> GetUserById(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is not null)
            return user;

        await Task.Yield();
        throw new UserNotFoundException();
    }

    public async Task<ApplicationUser?> TryGetUserByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user ?? null;
    }

    public async Task<IEnumerable<ApplicationUser>?> SearchUsers(string searchPhrase, IEnumerable<UserRole> roles,
        int maxResultCount)
    {
        var rolesMappedToGuids = roles.Select(r => UserRolesWithGuids.RolesWithGuids.Single(rg => rg.Value == r).Key)
            .ToArray();
        var result = await _userManager.Users.Where(c =>
                (c.UserName != null && c.Email != null &&
                 (c.Email.Contains(searchPhrase) || c.UserName.Contains(searchPhrase) ||
                  c.FirstName.Contains(searchPhrase) ||
                  c.LastName.Contains(searchPhrase)) &&
                 c.UserRoles.Any(r => rolesMappedToGuids.Contains(new Guid(r.Role.Id))))).Take(maxResultCount)
            .ToArrayAsync();
        return result;
    }

    public IUsersPaginatedList GetUsersPagedWithFilters(int page, int pageSize,
        IEnumerable<FilterItem> filters)
    {
        var filteredUsers = ApplyFiltering(filters);
        var users = filteredUsers
            .OrderBy(user => user.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(user => new
            {
                user.Email,
                user.Id,
                user.LockoutEnabled,
                user.EmailConfirmed,
                user.PhoneNumber,
                user.UserName,
                user.FirstName,
                user.LastName,
                user.BirthDate,
                RoleId = user.UserRoles.Select(userRole => userRole.RoleId).FirstOrDefault()
            })
            .ToArray();

        return new UsersPaginatedList()
        {
            Users = users.Select(user => new AppUser()
                {
                    Email = user.Email!,
                    Id = Guid.Parse(user.Id),
                    IsAccountDisabled = user.LockoutEnabled,
                    IsEmailConfirmed = user.EmailConfirmed,
                    PhoneNumber = user.PhoneNumber!,
                    UserName = user.UserName!,
                    Password = string.Empty,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate,
                    RoleId = user.RoleId is null ? Guid.Empty : Guid.Parse(user.RoleId)
                }).ToArray(),
            TotalCount = filteredUsers.Count()
        };
    }

    public async Task<bool> CheckUserPassword(Guid userGuid, string password)
    {
        var user = await TryGetUserById(userGuid);
        if (user == null) return false;

        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<bool> HasUser2FaEnabled(Guid userGuid)
    {
        var user = await TryGetUserById(userGuid);
        if (user == null) return false;
        return await _userManager.GetTwoFactorEnabledAsync(user);
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

    public async Task<bool> HasUserLocalPasswordConfiguredAsync(Guid userGuid)
    {
        var user = await _userManager.FindByIdAsync(userGuid.ToString());
        if (user == null) return false;

        return await _userManager.HasPasswordAsync(user);
    }

    public async Task<IEnumerable<UserLoginInfo>> GetUserConfiguredExternalLogins(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) return Enumerable.Empty<UserLoginInfo>();

        return await _userManager.GetLoginsAsync(user);
    }
}