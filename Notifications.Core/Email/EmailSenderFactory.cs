using Configuration.Shared.Managers;
using Configuration.Shared.Notifications;
using Notifications.Core.Email.MailTrap;
using Notifications.Core.Email.Smtp;

namespace Notifications.Core.Email;

internal class EmailSenderFactory : IEmailSenderFactory
{
    private readonly IEmailConfigurationGetter _emailConfigurationGetter;
    private readonly IAppConfigurationGetter _appConfigurationGetter;

    public EmailSenderFactory(IEmailConfigurationGetter emailConfigurationGetter, IAppConfigurationGetter appConfigurationGetter)
    {
        _emailConfigurationGetter = emailConfigurationGetter ?? throw new ArgumentNullException(nameof(emailConfigurationGetter));
        _appConfigurationGetter = appConfigurationGetter ?? throw new ArgumentNullException(nameof(appConfigurationGetter));
    }

    public async Task<IEmailSender> GetEmailSender()
    {
        var emailSendMode = await _emailConfigurationGetter.GetEmailSendModeFromConfig();
        var userWithPassword = await _emailConfigurationGetter.GetUserWithPasswordFromConfig();

        switch (emailSendMode)
        {
            case EmailSendMode.None:
                throw new InvalidOperationException("Email sending is disabled in the configuration.");
            case EmailSendMode.Smtp:
                var smtpConfiguration = await _appConfigurationGetter.GetSmtpConfiguration();
                return new SmtpEmailSender(userWithPassword.Email, userWithPassword.Password, smtpConfiguration.SmtpServerAddress, smtpConfiguration.SmtpServerPort.Value, smtpConfiguration.UseSsl);
            case EmailSendMode.MailTrap:
                return new MailTrapEmailSender();
            default:
                throw new InvalidOperationException($"Unsupported email send mode: {emailSendMode}");
        }
    }
}