using Configuration.Shared.Notifications;

namespace Configuration.Shared.Managers;

public interface IAppConfigurationGetter
{
    Task<IApplicationConfiguration> GetConfiguration();
    public Task<string> GetSavedEmailLogin();
    Task<ISmtpConfiguration?> GetSmtpConfiguration();
    Task<string> GetApplicationName();
}