using Data.DatabaseEnums;

namespace Data.Configuration.Mappers;

internal interface IAppParameterEnumMapper
{
    Abstract.Configuration.ApplicationParameter MapFromDatabaseEnum(ApplicationParameter parameter);
    ApplicationParameter MapToDatabaseEnum(Abstract.Configuration.ApplicationParameter parameter);
}