using Configuration.Repositories.ExternalLogin;
using Configuration.Shared.ExternalLogin;

namespace Configuration.ExternalLoginProviders;

public class ExternalLoginProvidersGetter : IExternalLoginProvidersGetter
{
    private readonly IExternalLoginProvidersRepository _repository;
    public ExternalLoginProvidersGetter(IExternalLoginProvidersRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<IExternalLoginProvider>> GetAllProviders()
    {
        var providersJson = await _repository.GetExternalLoginProviders();
        if (string.IsNullOrEmpty(providersJson))
        {
            return Enumerable.Empty<ExternalLoginProvider>();
        }

        try
        {
            var providers = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ExternalLoginProvider>>(providersJson);
            return providers ?? Enumerable.Empty<ExternalLoginProvider>();
        }
        catch (System.Text.Json.JsonException)
        {
            return Enumerable.Empty<ExternalLoginProvider>();
        }
    }
}