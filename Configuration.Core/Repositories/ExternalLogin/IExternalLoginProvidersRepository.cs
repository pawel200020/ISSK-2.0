namespace Configuration.Repositories.ExternalLogin;

public interface IExternalLoginProvidersRepository
{
    Task<string> GetExternalLoginProviders();
}