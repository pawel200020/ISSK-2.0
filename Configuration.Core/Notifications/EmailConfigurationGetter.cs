using Configuration.Encryption;
using Configuration.Shared;
using Configuration.Shared.AppParameters.Managers;
using Configuration.Shared.Notifications;
using Microsoft.Extensions.Configuration;

namespace Configuration.Notifications;

internal class EmailConfigurationGetter : IEmailConfigurationGetter
{
    private IAppParameterGetter _appParameterGetter;
    private readonly IEncryptionManager _encryptionManager;

    public EmailConfigurationGetter(IAppParameterGetter appParameterGetter, IEncryptionManager encryptionManager)
    {
        _appParameterGetter = appParameterGetter ?? throw new ArgumentNullException(nameof(appParameterGetter));
        _encryptionManager = encryptionManager ?? throw new ArgumentNullException(nameof(encryptionManager));
    }

    public async Task<UserWithPassword> GetUserWithPasswordFromConfig() =>
        new(
            (await _appParameterGetter.GetStringParameterValue(ApplicationParameter.EmailLogin))!,
              _encryptionManager.Decrypt(await _appParameterGetter.GetStringParameterValue(ApplicationParameter.EmailPassword),EncryptionKeys.EmailPasswordEncryptionKey));

    public async Task<EmailSendMode> GetEmailSendModeFromConfig() =>
        (EmailSendMode)await _appParameterGetter.GetIntParameterValue(ApplicationParameter.EmailMode);
}