using EventsSaver.Shared.Entities;

namespace EventsSaver.Core.Repositories.Seasons;

internal interface ISeasonsReadRepository
{
    Task<IEnumerable<SeasonEntity>> GetSeasonsFromDb();
}