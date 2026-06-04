using EventsSaver.Core.Caching;
using EventsSaver.Core.Repositories.Seasons;
using EventsSaver.Shared.Entities;
using EventsSaver.Shared.Managers;
using EventsSaver.Shared.Managers.Seasons;
using Microsoft.Extensions.Caching.Memory;

namespace EventsSaver.Core.Managers.Seasons;

internal class SeasonsGetter : ISeasonsGetter
{
    private readonly ISeasonsReadRepository _seasonsReadRepository;
    private readonly IMemoryCache _cache;
    private const int MaxPickerResultCount = 5;

    public SeasonsGetter(ISeasonsReadRepository seasonsReadRepository, IMemoryCache cache)
    {
        _seasonsReadRepository =
            seasonsReadRepository ?? throw new ArgumentNullException(nameof(seasonsReadRepository));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task<IEnumerable<ISeasonEntity>?> GetSeasonsPaged(int page, int pageSize)
    {
        if (_cache.TryGetValue(CacheKeys.Seasons, out IEnumerable<ISeasonEntity>? seasons) && seasons != null)
            return seasons.Skip((page - 1) * pageSize).Take(pageSize);
        await InitializeCache();
        
        return _cache.Get<IEnumerable<ISeasonEntity>>(CacheKeys.Seasons)?.Skip((page - 1) * pageSize).Take(pageSize);
    }
    
    public async Task<int> GetSeasonsCount()
    {
        if (_cache.TryGetValue(CacheKeys.Seasons, out IEnumerable<ISeasonEntity>? seasons) && seasons != null)
            return seasons.Count();
        await InitializeCache();
        return _cache.Get<IEnumerable<ISeasonEntity>>(CacheKeys.Seasons)?.Count() ?? 0;
    }

    public async Task<IEnumerable<ISeasonEntity>?> SearchSeason(string seasonName)
    {
        if (_cache.TryGetValue(CacheKeys.Seasons, out IEnumerable<ISeasonEntity>? seasons) && seasons != null)
            return seasons.Where(s => s.Name == seasonName).Take(MaxPickerResultCount);
        await InitializeCache();
        return _cache.Get<IEnumerable<ISeasonEntity>>(CacheKeys.Seasons)?.Where(s => s.Name == seasonName)
            .Take(MaxPickerResultCount);
    }

    private async Task InitializeCache()
    {
        var seasonsFromDb = (await _seasonsReadRepository.GetSeasonsFromDb()).ToArray();
        _cache.Set(CacheKeys.Seasons, seasonsFromDb);
    }
}