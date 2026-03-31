using BlazorBootstrap;
using Users.Shared.Models;

namespace Users.Core.Filters;

public class LastNameFilter : IFilter
{
    public IQueryable<ApplicationUser> Filter(IQueryable<ApplicationUser> collectionToFilter, FilterItem filter)
    {
        if (filter.PropertyName == nameof(ApplicationUser.LastName))
        {
            return filter.Operator switch
            {
                FilterOperator.Equals => collectionToFilter.Where(x => x.LastName.Equals(filter.Value)),
                FilterOperator.StartsWith => collectionToFilter.Where(x => x.LastName.StartsWith(filter.Value)),
                FilterOperator.EndsWith => collectionToFilter.Where(x => x.LastName.EndsWith(filter.Value)),
                FilterOperator.DoesNotContain => collectionToFilter.Where(x => !x.LastName.Contains(filter.Value)),
                FilterOperator.Contains => collectionToFilter.Where(x => x.LastName.Contains(filter.Value)),
                FilterOperator.NotEquals => collectionToFilter.Where(x => !x.LastName.Equals(filter.Value)),
                _ => throw new InvalidOperationException("operation not supported")
            };
        }

        throw new InvalidOperationException("wrong property to filter");
    }
}