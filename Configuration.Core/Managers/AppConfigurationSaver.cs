using Configuration.AppParameters.Managers;
using Configuration.Encryption;
using Configuration.Shared;
using Configuration.Shared.Managers;
using Configuration.Shared.Notifications;
using Newtonsoft.Json;

namespace Configuration.Managers;

internal class AppConfigurationSaver : IAppConfigurationSaver
{
    private readonly IAppParameterSaver _appParameterSaver;
    private readonly IEncryptionManager _encryptionManager;
    private readonly IAppConfigurationGetter _appConfigurationGetter;

    public AppConfigurationSaver(IAppParameterSaver appParameterSaver, IEncryptionManager encryptionManager,
        IAppConfigurationGetter appConfigurationGetter)
    {
        _appParameterSaver = appParameterSaver ?? throw new ArgumentNullException(nameof(appParameterSaver));
        _encryptionManager = encryptionManager ?? throw new ArgumentNullException(nameof(encryptionManager));
        _appConfigurationGetter =
            appConfigurationGetter ?? throw new ArgumentNullException(nameof(appConfigurationGetter));
    }

    public async Task<bool> SaveApplication(IApplicationConfiguration applicationConfiguration) =>
        await _appParameterSaver.SaveStringParameter(ApplicationParameter.ApplicationName,
            applicationConfiguration.ApplicationName) &&
        await _appParameterSaver.SaveBoolParameter(ApplicationParameter.IsWeatherEnabled,
            applicationConfiguration.IsWeatherEnabled) &&
        await _appParameterSaver.SaveBoolParameter(ApplicationParameter.IsRankEnabled,
            applicationConfiguration.IsRankingEnabled) &&
        await _appParameterSaver.SaveBoolParameter(ApplicationParameter.IsAnonymousRegisterEnabled,
            applicationConfiguration.IsAnonymousRegisterEnabled) &&
        await SavePassword(applicationConfiguration.EmailPassword,
            applicationConfiguration.EmailLogin, applicationConfiguration.SmtpConfiguration.SmtpServerAddress,
            applicationConfiguration.EmailSendMode) &&
        await _appParameterSaver.SaveStringParameter(ApplicationParameter.EmailLogin,
            applicationConfiguration.EmailLogin) &&
        await _appParameterSaver.SaveIntParameter(ApplicationParameter.EmailMode,
            (int)applicationConfiguration.EmailSendMode) &&
        await UpdateSmtpConfig(applicationConfiguration.EmailSendMode, applicationConfiguration.SmtpConfiguration) && 
        await UpdateEmailRedirect(applicationConfiguration.IsEmailRedirect, applicationConfiguration.EmailRedirectAddress);

    private string EncryptEmailPassword(string password)
        => _encryptionManager.Encrypt(password, EncryptionKeys.EmailPasswordEncryptionKey);

    private async Task<bool> UpdateEmailRedirect(bool isRedirectEnabled, string? redirectAddress)
    {
        if (!isRedirectEnabled)
            return await _appParameterSaver.SaveBoolParameter(ApplicationParameter.IsRedirectEmailEnabled, false) &&
                   await _appParameterSaver.SaveStringParameter(ApplicationParameter.EmailRedirectAddress, "");
        
        return await _appParameterSaver.SaveBoolParameter(ApplicationParameter.IsRedirectEmailEnabled, true) &&
               await _appParameterSaver.SaveStringParameter(ApplicationParameter.EmailRedirectAddress,
                   redirectAddress ?? "");
    }
    
    private async Task<bool> UpdateSmtpConfig(EmailSendMode emailSendMode, ISmtpConfiguration smtpConfiguration) =>
        emailSendMode != EmailSendMode.Smtp
            ? await _appParameterSaver.SaveStringParameter(ApplicationParameter.SmtpConfiguration, "")
            : await _appParameterSaver.SaveStringParameter(ApplicationParameter.SmtpConfiguration,
                JsonConvert.SerializeObject(smtpConfiguration) ?? "");

    private async Task<bool> SavePassword(string? password, string currentLogin, string currentServer,
        EmailSendMode currentMode)
    {
        var savedLogin = await _appConfigurationGetter.GetSavedEmailLogin();
        if (savedLogin != currentLogin || (currentMode == EmailSendMode.Smtp &&
                                           (await _appConfigurationGetter.GetSmtpConfiguration()).SmtpServerAddress !=
                                           currentServer))
            _appParameterSaver.SaveStringParameter(ApplicationParameter.EmailPassword,
                string.IsNullOrEmpty(password) ? "" : EncryptEmailPassword(password));

        return string.IsNullOrEmpty(password) || await _appParameterSaver.SaveStringParameter(
            ApplicationParameter.EmailPassword,
            EncryptEmailPassword(password));
    }
}