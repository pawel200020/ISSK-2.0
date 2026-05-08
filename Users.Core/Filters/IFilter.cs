using BlazorBootstrap;
using Users.Shared.Models;

namespace Users.Core.Filters;

internal interface IFilter
{
    IQueryable<ApplicationUser> Filter(IQueryable<ApplicationUser> collectionToFilter, FilterItem filter);
}