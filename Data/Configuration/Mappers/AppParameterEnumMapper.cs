using Data.DatabaseEnums;

namespace Data.Configuration.Mappers;

internal class AppParameterEnumMapper : IAppParameterEnumMapper //Tests!!
{
    public Abstract.Configuration.ApplicationParameter MapFromDatabaseEnum(ApplicationParameter parameter)
    {
        var intValue = (int)parameter;
        return (Abstract.Configuration.ApplicationParameter)intValue;
    }

    public ApplicationParameter MapToDatabaseEnum(Abstract.Configuration.ApplicationParameter parameter)
    {
        var intValue = (int)parameter;
        return (ApplicationParameter)intValue;
    }
}