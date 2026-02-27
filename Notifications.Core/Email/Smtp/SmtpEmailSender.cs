namespace Notifications.Core.Email.Smtp;

public class SmtpEmailSender : IEmailSender
{
    public SmtpEmailSender(string emai)
    {
        
    }
    public bool SendTestEmail(string recipientEmail)
    {
        // Implement SMTP email sending logic here
        // For example, using System.Net.Mail.SmtpClient
        return true; // Return true if email sent successfully, otherwise false
    }

    public bool SendNotificationEmail(string recipientEmail, string subject, string body)
    {
        return true;
    }
}