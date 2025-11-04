using System.Globalization;
using Abstract.Languages;
using Core.Caching;
using Data.Languages;
using Microsoft.Extensions.Caching.Memory;

namespace Core.Languages;

internal class SupportedLanguagesDownloader : ISupportedLanguagesDownloader
{
    private readonly IMemoryCache _cache;
    private readonly ISupportedLanguagesRepository _supportedLanguagesRepository;

    public SupportedLanguagesDownloader(IMemoryCache cache, ISupportedLanguagesRepository supportedLanguagesRepository)
    {
        _cache = cache;
        _supportedLanguagesRepository = supportedLanguagesRepository ?? throw new ArgumentNullException(nameof(supportedLanguagesRepository));
    }

    public async Task<IEnumerable<CultureInfo>> GetSupportedLanguages()
    {
        if (_cache.TryGetValue(CacheKeys.SupportedLanguages, out IEnumerable<CultureInfo> supportedLanguages))
        {
            return supportedLanguages;
        }
        var languagesFromDb = await _supportedLanguagesRepository.GetSupportedLanguages();
        supportedLanguages = languagesFromDb.Select(l => CultureInfo.CreateSpecificCulture(l.Code));
        
        _cache.Set(CacheKeys.SupportedLanguages, supportedLanguages, TimeSpan.FromDays(30));
            
        return supportedLanguages;
    }
}