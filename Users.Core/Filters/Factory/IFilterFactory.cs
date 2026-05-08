namespace Users.Core.Filters.Factory;

internal interface IFilterFactory
{
    IFilter Create(string propertyName);
}