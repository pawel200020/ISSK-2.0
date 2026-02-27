namespace Notifications.Core.Email.MailTrap;

public class MailTrapEmailSender : IEmailSender
{
    public bool SendTestEmail(string recipientEmail)
    {
        throw new NotImplementedException();
    }

    public bool SendNotificationEmail(string recipientEmail, string subject, string body)
    {
        throw new NotImplementedException();
    }
}