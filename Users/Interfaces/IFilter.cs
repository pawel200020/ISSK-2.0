using BlazorBootstrap;
using Users.Models;

namespace Users.Interfaces;

internal interface IFilter
{
    IQueryable<ApplicationUser> Filter(IQueryable<ApplicationUser> collectionToFilter, FilterItem filter);
}