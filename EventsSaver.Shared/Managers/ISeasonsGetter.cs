using EventsSaver.Shared.Entities;

namespace EventsSaver.Shared.Managers;

public interface ISeasonsGetter
{
    Task<IEnumerable<ISeasonEntity>?> GetSeasonsPaged(int page, int pageSize);
    Task<int> GetSeasonsCount();
    Task<IEnumerable<ISeasonEntity>?> SearchSeason(string seasonName);
}