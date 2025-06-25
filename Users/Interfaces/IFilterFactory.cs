namespace Users.Interfaces;

internal interface IFilterFactory
{
    IFilter Create(string propertyName);
}