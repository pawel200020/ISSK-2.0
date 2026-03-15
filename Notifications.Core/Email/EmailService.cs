using Configuration.Shared.Managers;
using Notifications.Core.Email.Templates;
using Notifications.Shared.Email;
using static Resources.PortalResources.PortalResources;

namespace Notifications.Core.Email;

internal class EmailService : IEmailService
{
    private readonly IEmailSenderFactory _emailSenderFactory;
    private readonly IAppConfigurationGetter _appConfigurationGetter;

    public EmailService(IEmailSenderFactory emailSenderFactory, IAppConfigurationGetter appConfigurationGetter)
    {
        _emailSenderFactory = emailSenderFactory ?? throw new ArgumentNullException(nameof(emailSenderFactory));
        _appConfigurationGetter =
            appConfigurationGetter ?? throw new ArgumentNullException(nameof(appConfigurationGetter));
    }

    public async Task<bool> SendTestEmail(string recipientEmail)
    {
        var emailSender = await _emailSenderFactory.GetEmailSender();

        await emailSender.SendEmail(recipientEmail, cTestEmail,
            StandardEmailTemplate.AutomatedEmailHtmlTemplate(cTestEmail,
                $"<p>\n{cTestEmailBody}</p>\n\n<p>\n{cTestEmailFooter}\n</p>",
                await _appConfigurationGetter.GetApplicationName())).ConfigureAwait(true);
        return true;
    }
}