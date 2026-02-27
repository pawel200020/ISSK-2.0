namespace Configuration.Shared.Managers;

public interface IAppConfigurationSaver
{
    Task<bool> SaveApplication(IApplicationConfiguration applicationConfiguration);
}