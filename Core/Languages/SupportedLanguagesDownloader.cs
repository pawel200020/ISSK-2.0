using System.Globalization;
using Abstract.Languages;
using Core.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace Core.Languages;

internal class SupportedLanguagesDownloader : ISupportedLanguagesDownloader
{
    private readonly IMemoryCache _cache;

    public SupportedLanguagesDownloader(IMemoryCache cache)
    {
        _cache = cache;
    }

    public IEnumerable<CultureInfo> GetSupportedLanguages()
    {
        
        //get enumerable from cache!!!
        if (_cache.TryGetValue(CacheKeys.SupportedLanguages, out IEnumerable<CultureInfo> supportedLanguages))
        {
            return supportedLanguages;
        }
        //if not cache 
        //getfromdb
            
        return [];
    }
}