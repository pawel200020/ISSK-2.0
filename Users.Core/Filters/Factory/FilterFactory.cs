using Users.Shared.Models;

namespace Users.Core.Filters.Factory;

internal class FilterFactory : IFilterFactory
{
    private readonly FirstNameFilter _firstNameFilter;
    private readonly LastNameFilter _lastNameFilter;
    private readonly UserNameFilter _userNameFilter;
    private readonly EmailFilter _emailFilter;
    private readonly BirthDateFilter _birthDateFilter;

    public FilterFactory(FirstNameFilter firstNameFilter, LastNameFilter lastNameFilter, UserNameFilter userNameFilter, EmailFilter emailFilter, BirthDateFilter birthDateFilter)
    {
        _firstNameFilter = firstNameFilter ?? throw new ArgumentNullException(nameof(firstNameFilter));
        _lastNameFilter = lastNameFilter ?? throw new ArgumentNullException(nameof(lastNameFilter));
        _userNameFilter = userNameFilter ?? throw new ArgumentNullException(nameof(userNameFilter));
        _emailFilter = emailFilter ?? throw new ArgumentNullException(nameof(emailFilter));
        _birthDateFilter = birthDateFilter ?? throw new ArgumentNullException(nameof(birthDateFilter));
    }

    public IFilter Create(string propertyName)
    {
        return propertyName switch
        {
            nameof(ApplicationUser.FirstName) => _firstNameFilter,
            nameof(ApplicationUser.LastName) => _lastNameFilter,
            nameof(ApplicationUser.UserName) => _userNameFilter,
            nameof(ApplicationUser.Email) => _emailFilter,
            nameof(ApplicationUser.BirthDate) => _birthDateFilter,
            _ => throw new InvalidOperationException("incorrect operation key")
        };
    }
    
}