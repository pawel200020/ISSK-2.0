namespace Abstract.Configuration.Managers;

public interface IAppConfigurationGetter
{
    Task<IApplicationConfiguration> GetConfiguration();
}