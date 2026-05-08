namespace Notifications.Core.Email.MailTrap;

public class MailTrapEmailSender : IEmailSender
{
    public async Task SendEmail(string recipientEmail, string subject, string body)
    {
        // Ensure the exception is thrown asynchronously so callers always receive it as a faulted Task
        // rather than having a synchronous throw that may crash the caller if not handled synchronously.
        await Task.FromException(new NotImplementedException());
        //await Task.Yield();
        //throw new InvalidOperationException();
    }
}