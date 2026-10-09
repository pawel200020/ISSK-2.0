using Configuration.Shared;
using Configuration.Shared.AppParameters.Managers;
using Configuration.Shared.Entities;
using Configuration.Shared.ExternalLogin;
using Configuration.Shared.Managers;
using Configuration.Shared.Notifications;
using Newtonsoft.Json;

namespace Configuration.Managers;

internal class AppConfigurationGetter : IAppConfigurationGetter
{
    private IAppParameterGetter _appParameterGetter;
    private IExternalLoginProvidersGetter _externalLoginProvider;

    public AppConfigurationGetter(IAppParameterGetter appParameterGetter, IExternalLoginProvidersGetter externalLoginProvider)
    {
        _appParameterGetter = appParameterGetter ?? throw new ArgumentNullException(nameof(appParameterGetter));
        _externalLoginProvider = externalLoginProvider ?? throw new ArgumentNullException(nameof(externalLoginProvider));
    }

    public async Task<IApplicationConfiguration> GetConfiguration() =>
        new ApplicationConfiguration()
        {
            ApplicationName =
                (await _appParameterGetter.GetStringParameterValue(ApplicationParameter.ApplicationName))!,
            IsWeatherEnabled = await _appParameterGetter.GetBoolParameterValue(ApplicationParameter.IsWeatherEnabled),
            IsRankingEnabled = await _appParameterGetter.GetBoolParameterValue(ApplicationParameter.IsRankEnabled),
            IsAnonymousRegisterEnabled =
                await _appParameterGetter.GetBoolParameterValue(ApplicationParameter.IsAnonymousRegisterEnabled),
            EmailLogin = await _appParameterGetter.GetStringParameterValue(ApplicationParameter.EmailLogin),
            EmailSendMode =
                (EmailSendMode)await _appParameterGetter.GetIntParameterValue(ApplicationParameter.EmailMode),
            SmtpConfiguration =
                JsonConvert.DeserializeObject<SmtpConfiguration>(
                    await _appParameterGetter.GetStringParameterValue(ApplicationParameter.SmtpConfiguration) ?? "") ??
                new SmtpConfiguration(),
            IsEmailRedirect =
                await _appParameterGetter.GetBoolParameterValue(ApplicationParameter.IsRedirectEmailEnabled),
            EmailRedirectAddress =
                await _appParameterGetter.GetStringParameterValue(ApplicationParameter.EmailRedirectAddress),
            ExternalLoginProviders =
                await _externalLoginProvider.GetAllProviders()
        };

    public async Task<string> GetSavedEmailLogin() =>
        (await _appParameterGetter.GetStringParameterValue(ApplicationParameter.EmailLogin)) ?? "";

    public async Task<ISmtpConfiguration?> GetSmtpConfiguration() =>
        JsonConvert.DeserializeObject<SmtpConfiguration>(
            await _appParameterGetter.GetStringParameterValue(ApplicationParameter.SmtpConfiguration) ?? "");

    public async Task<string> GetApplicationName()
        => (await _appParameterGetter.GetStringParameterValue(ApplicationParameter.ApplicationName))!;

    public Task<bool> IsAnonymousRegisterEnabled() =>
        _appParameterGetter.GetBoolParameterValue(ApplicationParameter.IsAnonymousRegisterEnabled);

    public async Task<string?> GetOverridenEmailReceiver()
    {
        return await _appParameterGetter.GetStringParameterValue(ApplicationParameter.EmailRedirectAddress);
    }
}