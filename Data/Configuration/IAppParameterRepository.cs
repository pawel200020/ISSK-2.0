using Abstract.Configuration;

namespace Data.Configuration;

public interface IAppParameterRepository
{
    Task<string?> GetStringParameterValue(ApplicationParameter name);
    Task<int> GetIntParameterValue(ApplicationParameter name);
    Task<bool> GetBoolParameterValue(ApplicationParameter name);
    Task<bool> SetStringParameterValue(ApplicationParameter name, string value);
    Task<bool>  SetIntParameterValue(ApplicationParameter name, int value);
    Task<bool>  SetBoolParameterValue(ApplicationParameter name, bool value);
}