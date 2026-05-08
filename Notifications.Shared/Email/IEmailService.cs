namespace Notifications.Shared.Email;

public interface IEmailService
{
    Task<bool> SendTestEmail(string recipientEmail);
    Task SendEmail(string recipientEmail, string subject, string body);
}