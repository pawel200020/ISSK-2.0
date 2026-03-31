using BlazorBootstrap;
using Users.Shared.Models;

namespace Users.Core.Filters;

public class EmailFilter : IFilter
{
    public IQueryable<ApplicationUser> Filter(IQueryable<ApplicationUser> collectionToFilter, FilterItem filter)
    {
        if (filter.PropertyName == nameof(ApplicationUser.Email))
        {
            return filter.Operator switch
            {
                FilterOperator.Equals => collectionToFilter.Where(x => x.Email!.Equals(filter.Value)),
                FilterOperator.StartsWith => collectionToFilter.Where(x => x.Email!.StartsWith(filter.Value)),
                FilterOperator.EndsWith => collectionToFilter.Where(x => x.Email!.EndsWith(filter.Value)),
                FilterOperator.DoesNotContain => collectionToFilter.Where(x => !x.Email!.Contains(filter.Value)),
                FilterOperator.Contains => collectionToFilter.Where(x => x.Email!.Contains(filter.Value)),
                FilterOperator.NotEquals => collectionToFilter.Where(x => !x.Email!.Equals(filter.Value)),
                _ => throw new InvalidOperationException("operation not supported")
            };
        }

        throw new InvalidOperationException("wrong property to filter");
    }
}