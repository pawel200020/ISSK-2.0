using EventsSaver.Core.Caching;
using EventsSaver.Core.Repositories.Seasons;
using EventsSaver.Shared.Entities;
using EventsSaver.Shared.Managers.Seasons;
using Microsoft.Extensions.Caching.Memory;

namespace EventsSaver.Core.Managers.Seasons;

internal class SeasonCreator : ISeasonCreator
{
    private readonly ISeasonsWriteRepository _seasonsWriteRepository;
    private readonly IMemoryCache _cache;

    public SeasonCreator(ISeasonsWriteRepository seasonsWriteRepository, IMemoryCache cache)
    {
        _seasonsWriteRepository =
            seasonsWriteRepository ?? throw new ArgumentNullException(nameof(seasonsWriteRepository));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task CreateSeason(ISeasonEntity season)
    {
        await _seasonsWriteRepository.CreateNewSeason(season);
        if (_cache.TryGetValue(CacheKeys.Seasons, out IEnumerable<ISeasonEntity>? seasons) && seasons != null)
            _cache.Set(CacheKeys.Seasons, seasons.Append(season));
    }
}