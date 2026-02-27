using Configuration.Shared;
using Configuration.Shared.AppParameters.Managers;
using Configuration.Shared.Notifications;

namespace Configuration.Notifications;

public class EmailConfigurationGetter : IEmailConfigurationGetter
{
    private IAppParameterGetter _appParameterGetter;

    public EmailConfigurationGetter(IAppParameterGetter appParameterGetter)
    {
        _appParameterGetter = appParameterGetter;
    }

    public async Task<UserWithPassword> GetUserWithPasswordFromConfig() =>
        new(
            (await _appParameterGetter.GetStringParameterValue(ApplicationParameter.EmailLogin))!,
            await _appParameterGetter.GetStringParameterValue(ApplicationParameter.EmailPassword));

    public async Task<EmailSendMode> GetEmailSendModeFromConfig() =>
        (EmailSendMode)await _appParameterGetter.GetIntParameterValue(ApplicationParameter.EmailMode);
}