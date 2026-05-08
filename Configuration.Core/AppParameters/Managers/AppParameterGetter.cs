using Configuration.Shared;
using Configuration.Shared.AppParameters.Managers;
using Data.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace Configuration.AppParameters.Managers;

internal class AppParameterGetter : IAppParameterGetter
{
    private IMemoryCache _memoryCache;
    private readonly IAppParameterRepository _appParameterRepository;

    public AppParameterGetter(IMemoryCache memoryCache, IAppParameterRepository appParameterRepository)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        _appParameterRepository = appParameterRepository ?? throw new ArgumentNullException(nameof(appParameterRepository));
    }
    
    public async Task<string?> GetStringParameterValue(ApplicationParameter parameter)
    {
        var cachedValue = _memoryCache.Get<string>(Enum.GetName(parameter)!);
        if (cachedValue == null)
        {
            cachedValue = await _appParameterRepository.GetStringParameterValue(parameter);
            _memoryCache.Set(Enum.GetName(parameter)!, cachedValue);
        }

        return cachedValue;
    }
    public async Task<int> GetIntParameterValue(ApplicationParameter parameter)
    {
        var cachedValue = _memoryCache.Get<int?>(Enum.GetName(parameter)!);
        if (cachedValue == null)
        {
            cachedValue = await _appParameterRepository.GetIntParameterValue(parameter);
            _memoryCache.Set(Enum.GetName(parameter)!, cachedValue);
        }

        return cachedValue.Value;
    }

    public async Task<bool> GetBoolParameterValue(ApplicationParameter parameter)
    {
        var cachedValue = _memoryCache.Get<bool?>(Enum.GetName(parameter)!);
        if (cachedValue == null)
        {
            cachedValue = await _appParameterRepository.GetBoolParameterValue(parameter);
            _memoryCache.Set(Enum.GetName(parameter)!, cachedValue);
        }

        return cachedValue.Value;
    }
}