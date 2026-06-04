using Data.Entites.EventSaver;
using Data.Entites.EventSaver.Enums;
using EventsSaver.Shared.Entities;
using EventsSaver.Shared.Entities.Enums;
using Users.Shared.Models;

namespace EventsSaver.Core.Entities;

internal static class LineExtensions
{
    public static ILineEntity ToDomainEntity(this LineDb lineDb)
    {
        return new LineEntity(lineDb.Id, lineDb.Number, lineDb.Date, lineDb.Vehicle, new Guid(lineDb.Supervisor.Id), lineDb.Season.ToDomainEntity(), (LineType) lineDb.LineType, lineDb.IsActive);
    }

    public static LineDb ToDbEntity(this LineEntity entity)
    {
        return new LineDb()
        {
            Id = entity.Id,
            Number = entity.Number,
            Date = entity.Date,
            Vehicle = entity.Vehicle,
            SupervisorId = entity.SupervisorId.ToString(),
            SeasonId = entity.Season.SeasonId,
            LineType = (LineTypeDb)entity.LineType,
            IsActive = entity.IsActive
        };
    }
}