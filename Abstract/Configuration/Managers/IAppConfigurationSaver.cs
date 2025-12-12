namespace Abstract.Configuration.Managers;

public interface IAppConfigurationSaver
{
    Task<bool> SaveApplication(IApplicationConfiguration applicationConfiguration);
}