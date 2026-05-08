using Configuration.Shared.Notifications;

namespace Configuration.Shared.Managers;

public interface IAppConfigurationGetter
{
    Task<IApplicationConfiguration> GetConfiguration();
    Task<string> GetSavedEmailLogin();
    Task<ISmtpConfiguration?> GetSmtpConfiguration();
    Task<string> GetApplicationName();
    Task<bool> IsAnonymousRegisterEnabled();
    Task<string?> GetOverridenEmailReceiver();
}