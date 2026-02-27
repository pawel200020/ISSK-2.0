namespace Notifications.Shared.Email;

public interface IEmailSender
{
    public Task<bool> SendTestEmailAsync(string email);
}