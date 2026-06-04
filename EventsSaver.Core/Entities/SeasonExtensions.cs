using Data.Entites.EventSaver;
using EventsSaver.Shared.Entities;

namespace EventsSaver.Core.Entities;

public static class SeasonExtensions
{
    public static SeasonEntity ToDomainEntity(this SeasonDb seasonDb) =>
        new(seasonDb.Id, seasonDb.Name, seasonDb.StartDate, seasonDb.EndDate);
    
}