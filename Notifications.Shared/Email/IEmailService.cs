namespace Notifications.Shared.Email;

public interface IEmailService
{
    Task<bool> SendTestEmail(string recipientEmail);
}