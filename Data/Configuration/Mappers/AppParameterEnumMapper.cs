using Data.DatabaseEnums;
using ConfigurationShared = Configuration.Shared;

namespace Data.Configuration.Mappers;

internal class AppParameterEnumMapper : IAppParameterEnumMapper
{
    public ConfigurationShared.ApplicationParameter MapFromDatabaseEnum(ApplicationParameter parameter)
    {
        var intValue = (int)parameter;
        return (ConfigurationShared.ApplicationParameter)intValue;
    }

    public ApplicationParameter MapToDatabaseEnum(ConfigurationShared.ApplicationParameter parameter)
    {
        var intValue = (int)parameter;
        return (ApplicationParameter)intValue;
    }
}