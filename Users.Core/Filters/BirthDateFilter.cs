using BlazorBootstrap;
using Users.Shared.Models;

namespace Users.Core.Filters;

public class BirthDateFilter : IFilter
{
    public IQueryable<ApplicationUser> Filter(IQueryable<ApplicationUser> collectionToFilter, FilterItem filter)
    {
        if (filter.PropertyName == nameof(ApplicationUser.BirthDate))
        {
            if (!DateOnly.TryParse(filter.Value, out var parsedDate))
                throw new InvalidOperationException(
                    $"cannot parse argunment to date. Source: {nameof(BirthDateFilter)}");
            
            return filter.Operator switch
            { 
                FilterOperator.Equals => collectionToFilter.Where(x => x.BirthDate == parsedDate),
                FilterOperator.LessThan => collectionToFilter.Where(x => x.BirthDate < parsedDate),
                FilterOperator.GreaterThan => collectionToFilter.Where(x => x.BirthDate > parsedDate),
                FilterOperator.NotEquals => collectionToFilter.Where(x => x.BirthDate != parsedDate),
                FilterOperator.LessThanOrEquals => collectionToFilter.Where(x => x.BirthDate <= parsedDate),
                FilterOperator.GreaterThanOrEquals => collectionToFilter.Where(x => x.BirthDate >= parsedDate),
                _ => throw new InvalidOperationException("operation not supported")
            };
        }

        throw new InvalidOperationException("wrong property to filter");
    }
}