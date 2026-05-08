namespace Configuration.Shared.Notifications;

public interface IEmailConfigurationGetter
{
    Task<UserWithPassword> GetUserWithPasswordFromConfig();
    Task<EmailSendMode> GetEmailSendModeFromConfig();
}