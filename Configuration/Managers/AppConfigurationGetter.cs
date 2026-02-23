using Abstract.Configuration;
using Abstract.Configuration.AppParameters.Managers;
using Abstract.Configuration.Entities;
using Abstract.Configuration.Managers;

namespace Configuration.Managers;

internal class AppConfigurationGetter : IAppConfigurationGetter
{
    private IAppParameterGetter _appParameterGetter;

    public AppConfigurationGetter(IAppParameterGetter appParameterGetter)
    {
        _appParameterGetter = appParameterGetter ?? throw new ArgumentNullException(nameof(appParameterGetter));
    }

    public async Task<IApplicationConfiguration> GetConfiguration() =>
        new ApplicationConfiguration()
        {
            ApplicationName = await _appParameterGetter.GetStringParameterValue(ApplicationParameter.ApplicationName),
            IsWeatherEnabled = await _appParameterGetter.GetBoolParameterValue(ApplicationParameter.IsWeatherEnabled),
            IsRankingEnabled = await _appParameterGetter.GetBoolParameterValue(ApplicationParameter.IsRankEnabled),
            IsAnonymousRegisterEnabled = await _appParameterGetter.GetBoolParameterValue(ApplicationParameter.IsAnonymousRegisterEnabled),
            EmailLogin = await _appParameterGetter.GetStringParameterValue(ApplicationParameter.EmailLogin),
            EmailSendMode = await _appParameterGetter.GetIntParameterValue(ApplicationParameter.EmailMode),
        };
}