using Data.DatabaseEnums;
using ConfigurationShared = Configuration.Shared;

namespace Data.Configuration.Mappers;

internal interface IAppParameterEnumMapper
{
    ConfigurationShared.ApplicationParameter MapFromDatabaseEnum(ApplicationParameter parameter);
    ApplicationParameter MapToDatabaseEnum(ConfigurationShared.ApplicationParameter parameter);
}