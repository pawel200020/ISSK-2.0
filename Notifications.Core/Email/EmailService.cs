using Configuration.Shared.Managers;
using Notifications.Shared.Email;
using Notifications.Shared.Templates;
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
        var overridenRecipient = await _appConfigurationGetter.GetOverridenEmailReceiver();

        await emailSender.SendEmail(
            await GetEmailRecipient(recipientEmail),
            cTestEmail,
            StandardEmailTemplate.AutomatedEmailHtmlTemplate(cTestEmail,
                $"<p>\n{cTestEmailBody}</p>\n\n<p>\n{cTestEmailFooter}\n</p>",
                await _appConfigurationGetter.GetApplicationName())).ConfigureAwait(true);
        return true;
    }

    public async Task SendEmail(string recipientEmail, string subject, string body)
    {
        var emailSender = await _emailSenderFactory.GetEmailSender();
        await emailSender.SendEmail(await GetEmailRecipient(recipientEmail), subject, body);
    }

    private async Task<string> GetEmailRecipient(string email)
    {
        var overridenRecipient = await _appConfigurationGetter.GetOverridenEmailReceiver();
        return !string.IsNullOrEmpty(overridenRecipient)
            ? overridenRecipient
            : email;
    }
}