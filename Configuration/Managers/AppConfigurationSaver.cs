using Abstract.Configuration;
using Abstract.Configuration.Managers;
using Configuration.AppParameters.Managers;

namespace Configuration.Managers;

internal class AppConfigurationSaver : IAppConfigurationSaver
{
    IAppParameterSaver _appParameterSaver;

    public AppConfigurationSaver(IAppParameterSaver appParameterSaver)
    {
        _appParameterSaver = appParameterSaver ?? throw new ArgumentNullException(nameof(appParameterSaver));
    }

    public async Task<bool> SaveApplication(IApplicationConfiguration applicationConfiguration) =>
        await _appParameterSaver.SaveStringParameter(ApplicationParameter.ApplicationName,
            applicationConfiguration.ApplicationName) &&
        await _appParameterSaver.SaveBoolParameter(ApplicationParameter.IsWeatherEnabled,
            applicationConfiguration.IsWeatherEnabled) &&
        await _appParameterSaver.SaveBoolParameter(ApplicationParameter.IsRankEnabled,
            applicationConfiguration.IsRankingEnabled);
}