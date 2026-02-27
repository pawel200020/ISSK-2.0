using Configuration.AppParameters.Managers;
using Configuration.Encryption;
using Configuration.Shared;
using Configuration.Shared.Managers;

namespace Configuration.Managers;

internal class AppConfigurationSaver : IAppConfigurationSaver
{
    private readonly IAppParameterSaver _appParameterSaver;
    private readonly IEncryptionManager _encryptionManager;

    public AppConfigurationSaver(IAppParameterSaver appParameterSaver, IEncryptionManager encryptionManager)
    {
        _appParameterSaver = appParameterSaver ?? throw new ArgumentNullException(nameof(appParameterSaver));
        _encryptionManager = encryptionManager ?? throw new ArgumentNullException(nameof(encryptionManager));
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
        await _appParameterSaver.SaveStringParameter(ApplicationParameter.EmailLogin,
            applicationConfiguration.EmailLogin) && await SavePassword(applicationConfiguration.EmailPassword) &&
        await _appParameterSaver.SaveIntParameter(ApplicationParameter.EmailMode, (int)applicationConfiguration.EmailSendMode);
    
    private string EncryptEmailPassword(string password) 
        => _encryptionManager.Encrypt(password, EncryptionKeys.EmailPasswordEncryptionKey);

    private Task<bool> SavePassword(string? password) =>
        string.IsNullOrEmpty(password) 
            ? Task.FromResult(true) 
            : _appParameterSaver.SaveStringParameter(ApplicationParameter.EmailPassword, EncryptEmailPassword(password));
}
