namespace Notifications.Core.Email;

public interface IEmailSender
{
    Task SendEmail(string recipientEmail, string subject, string body);
}