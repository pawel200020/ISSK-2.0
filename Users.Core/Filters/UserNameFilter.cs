using BlazorBootstrap;
using Users.Shared.Models;

namespace Users.Core.Filters;

public class UserNameFilter : IFilter
{
    public IQueryable<ApplicationUser> Filter(IQueryable<ApplicationUser> collectionToFilter, FilterItem filter)
    {
        if (filter.PropertyName == nameof(ApplicationUser.UserName))
        {
            return filter.Operator switch
            {
                FilterOperator.Equals => collectionToFilter.Where(x => x.UserName!.Equals(filter.Value)),
                FilterOperator.StartsWith => collectionToFilter.Where(x => x.UserName!.StartsWith(filter.Value)),
                FilterOperator.EndsWith => collectionToFilter.Where(x => x.UserName!.EndsWith(filter.Value)),
                FilterOperator.DoesNotContain => collectionToFilter.Where(x => !x.UserName!.Contains(filter.Value)),
                FilterOperator.Contains => collectionToFilter.Where(x => x.UserName!.Contains(filter.Value)),
                FilterOperator.NotEquals => collectionToFilter.Where(x => !x.UserName!.Equals(filter.Value)),
                _ => throw new InvalidOperationException("operation not supported")
            };
        }

        throw new InvalidOperationException("wrong property to filter");
    }
}