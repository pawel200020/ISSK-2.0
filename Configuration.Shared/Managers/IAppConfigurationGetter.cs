namespace Configuration.Shared.Managers;

public interface IAppConfigurationGetter
{
    Task<IApplicationConfiguration> GetConfiguration();
}