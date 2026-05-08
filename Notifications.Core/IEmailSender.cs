namespace Notifications.Core;

public interface IEmailSender
{
    public Task<bool> SendTestEmailAsync(string email);
}