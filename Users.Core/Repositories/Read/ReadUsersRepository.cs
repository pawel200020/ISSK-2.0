using BlazorBootstrap;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Users.Core.Filters.Factory;
using Users.Shared.Exceptions;
using Users.Shared.Models;

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

    public async Task<ApplicationUser> TryGetUserByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is not null) 
            return user;
        
        await Task.Yield();
        throw new UserNotFoundException();
    }


    public IUsersPaginatedList GetUsersPagedWithFilters(int page, int pageSize,
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
    
    private int GetTotalRowsNumber() => _userManager.Users.Count();
}