using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Notifications.Core.Email.Smtp;

internal class SmtpEmailSender : IEmailSender
{
    private readonly string _emailAddress;
    private readonly string _password;
    private readonly string _server;
    private readonly int _port;
    private readonly bool _useSsl;


    public SmtpEmailSender(string emailAddress, string password, string server, int port, bool useSsl)
    {
        _emailAddress = emailAddress;
        _password = password;
        _server = server;
        _port = port;
        _useSsl = useSsl;
    }

    public async Task SendEmail(string recipientEmail, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("", _emailAddress));
        message.To.Add(new MailboxAddress("", recipientEmail));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = body;
        message.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            try
            {
                var socketOptions =
                    _useSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTlsWhenAvailable;
                client.Connect(_server, _port, socketOptions);
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                var user = _emailAddress.Trim();
                var pass = _password.Trim();

                var mechanisms = client.AuthenticationMechanisms?.ToList() ?? new List<string>();
                try
                {
                    if (mechanisms.Contains("LOGIN", StringComparer.OrdinalIgnoreCase) && _useSsl)
                        await client.AuthenticateAsync(user, pass, CancellationToken.None).ConfigureAwait(false);

                    else if (mechanisms.Contains("PLAIN", StringComparer.OrdinalIgnoreCase))
                        await client.AuthenticateAsync(new SaslMechanismPlain(user, pass)).ConfigureAwait(false);
                }
                catch (AuthenticationException authEx)
                {
                    await Task.Yield();
                    var mechList = mechanisms.Count > 0 ? string.Join(", ", mechanisms) : "(none advertised)";
                    throw new InvalidOperationException(
                        $"SMTP authentication failed for user '{user}' against {_server}:{_port}. Server-supported mechanisms: {mechList}. See inner exception for details.",
                        authEx);
                }

                await client.SendAsync(message).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);

            }
            catch (SslHandshakeException ex)
            {
                // SSL/TLS handshake problems (wrong port / server requires different security)
                await Task.Yield();
                throw new InvalidOperationException(
                    $"SMTP SSL/TLS handshake failed connecting to {_server}:{_port} - {ex.Message}", ex);
            }
            catch (SmtpCommandException ex)
            {
                // Specific SMTP command failure with status code and response
                await Task.Yield();
                throw new InvalidOperationException(
                    $"SMTP command failed (SmtpCommandException) StatusCode={ex.StatusCode}: {ex.Message}", ex);
            }
            catch (CommandException ex)
            {
                // Commands like EHLO/MAIL/RCPT failures
                await Task.Yield();
                throw new InvalidOperationException("SMTP command failed: " + ex.Message, ex);
            }
            catch (AuthenticationException ex)
            {
                // Authentication failed (bad credentials / account configuration)
                await Task.Yield();
                throw new InvalidOperationException(
                    "SMTP authentication failed. Ensure the username and password are correct and the account allows SMTP authentication (app password/less-secure-apps or OAuth).",
                    ex);
            }
            catch (Exception ex)
            {
                await Task.Yield();
                throw new InvalidOperationException($"Failed to send SMTP test email: {ex.Message}", ex);
            }
        }
    }
}
