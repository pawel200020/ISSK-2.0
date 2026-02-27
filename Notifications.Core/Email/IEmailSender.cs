namespace Notifications.Core.Email;

public interface IEmailSender
{
    bool SendTestEmail(string recipientEmail);
    bool SendNotificationEmail(string recipientEmail, string subject, string body);
}