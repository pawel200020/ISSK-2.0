namespace Abstract.Configuration.AppParameters.Managers;

public interface IAppParameterGetter
{
    Task<string> GetStringParameterValue(ApplicationParameter parameter);
    Task<int> GetIntParameterValue(ApplicationParameter parameter);
    Task<bool> GetBoolParameterValue(ApplicationParameter parameter);
}