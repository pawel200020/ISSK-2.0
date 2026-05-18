using EventsSaver.Shared.Entities;

namespace EventsSaver.Core.Repositories.Seasons;

internal interface ISeasonsWriteRepository
{
    Task CreateNewSeason(ISeasonEntity season);
    Task DeleteSeason(Guid seasonId);
}