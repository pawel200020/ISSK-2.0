namespace Notifications.Core.Email;

internal interface IEmailSenderFactory
{
    Task<IEmailSender> GetEmailSender();
}