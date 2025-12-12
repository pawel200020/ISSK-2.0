using Abstract.Configuration;

namespace Configuration.AppParameters.Managers;

internal interface IAppParameterSaver
{
    Task<bool> SaveStringParameter(ApplicationParameter appParameter, string value);
    Task<bool> SaveIntParameter(ApplicationParameter appParameter, int value);
    Task<bool> SaveBoolParameter(ApplicationParameter appParameter, bool value);
}