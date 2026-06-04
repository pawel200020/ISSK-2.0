using EventsSaver.Core.Caching;
using EventsSaver.Core.Repositories.Seasons;
using EventsSaver.Shared.Entities;
using EventsSaver.Shared.Managers;
using EventsSaver.Shared.Managers.Seasons;
using Microsoft.Extensions.Caching.Memory;

namespace EventsSaver.Core.Managers.Seasons;

internal class SeasonRemover : ISeasonRemover
{
    private readonly ISeasonsWriteRepository _seasonsWriteRepository;
    private readonly IMemoryCache _cache;

    public SeasonRemover(ISeasonsWriteRepository seasonsWriteRepository, IMemoryCache cache)
    {
        _seasonsWriteRepository =
            seasonsWriteRepository ?? throw new ArgumentNullException(nameof(seasonsWriteRepository));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task RemoveSeason(Guid seasonId)
    {
        await _seasonsWriteRepository.DeleteSeason(seasonId);
        if (_cache.TryGetValue(CacheKeys.Seasons, out IEnumerable<ISeasonEntity>? seasons) && seasons != null)
            _cache.Set(CacheKeys.Seasons, seasons.Where(s => s.SeasonId != seasonId));
    }
}