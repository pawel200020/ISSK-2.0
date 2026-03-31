using BlazorBootstrap;
using Users.Shared.Models;

namespace Users.Core.Filters;

internal class FirstNameFilter : IFilter
{
    public IQueryable<ApplicationUser> Filter(IQueryable<ApplicationUser> collectionToFilter, FilterItem filter)
    {
        if (filter.PropertyName == nameof(ApplicationUser.FirstName))
        {
            return filter.Operator switch
            {
                FilterOperator.Equals => collectionToFilter.Where(x => x.FirstName.Equals(filter.Value)),
                FilterOperator.StartsWith => collectionToFilter.Where(x => x.FirstName.StartsWith(filter.Value)),
                FilterOperator.EndsWith => collectionToFilter.Where(x => x.FirstName.EndsWith(filter.Value)),
                FilterOperator.DoesNotContain => collectionToFilter.Where(x => !x.FirstName.Contains(filter.Value)),
                FilterOperator.Contains => collectionToFilter.Where(x => x.FirstName.Contains(filter.Value)),
                FilterOperator.NotEquals => collectionToFilter.Where(x => !x.FirstName.Equals(filter.Value)),
                _ => throw new InvalidOperationException("operation not supported")
            };
        }

        throw new InvalidOperationException("wrong property to filter");
    }
}