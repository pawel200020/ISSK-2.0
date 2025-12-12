using Abstract.Configuration;
using Data.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace Configuration.AppParameters.Managers;

internal class AppParameterSaver : IAppParameterSaver
{
    private readonly IMemoryCache _memoryCache;
    private readonly IAppParameterRepository _appParameterRepository;

    public AppParameterSaver(IMemoryCache memoryCache, IAppParameterRepository appParameterRepository)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        _appParameterRepository = appParameterRepository ?? throw new ArgumentNullException(nameof(appParameterRepository));
    }

    public async Task<bool> SaveStringParameter(ApplicationParameter appParameter, string value)
    {
        await _appParameterRepository.SetStringParameterValue(appParameter, value);
        UpdateCache(appParameter,value);
        return true;
    }
    
    public async Task<bool> SaveIntParameter(ApplicationParameter appParameter, int value)
    {
        await _appParameterRepository.SetIntParameterValue(appParameter, value);
        UpdateCache(appParameter,value);
        return true;
    }
    
    public async Task<bool> SaveBoolParameter(ApplicationParameter appParameter, bool value)
    {
        await _appParameterRepository.SetBoolParameterValue(appParameter, value);
        UpdateCache(appParameter,value);
        return true;
    }

    private void UpdateCache(ApplicationParameter parameter, object value)
    {
        _memoryCache.Set(Enum.GetName(parameter)!, value);
    }
}