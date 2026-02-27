using Configuration.Shared.Notifications;
using Notifications.Core.Email.MailTrap;
using Notifications.Core.Email.Smtp;

namespace Notifications.Core.Email;

internal class EmailSenderFactory : IEmailSenderFactory
{
    private readonly IEmailConfigurationGetter _emailConfigurationGetter;

    public EmailSenderFactory(IEmailConfigurationGetter emailConfigurationGetter)
    {
        _emailConfigurationGetter = emailConfigurationGetter;
    }

    public async Task<IEmailSender> GetEmailSender()
    {
        var emailSendMode = await _emailConfigurationGetter.GetEmailSendModeFromConfig();
        var userWithPassword = await _emailConfigurationGetter.GetUserWithPasswordFromConfig();

        return emailSendMode switch
        {
            EmailSendMode.None=> throw new InvalidOperationException("Email sending is disabled in the configuration."),
            EmailSendMode.MailTrap => new MailTrapEmailSender(),
            EmailSendMode.Smtp => new SmtpEmailSender(),
            _ => throw new InvalidOperationException($"Unsupported email send mode: {emailSendMode}")
        };
    }
}