using System.Text.Json;
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

    public async Task<IEnumerable<IExternalLoginProvider>> GetAllProvidersWithoutSensitiveData() 
        => await GetAllProviders(true);
    
    private async Task<IEnumerable<IExternalLoginProvider>> GetAllProviders(bool shouldCleanSensitiveData)
    {
        var providersJson = await _repository.GetExternalLoginProviders();
        if (string.IsNullOrWhiteSpace(providersJson))
        {
            return Enumerable.Empty<ExternalLoginProvider>();
        }

        try
        {
            using var document = JsonDocument.Parse(providersJson);
            return ParseProviders(document.RootElement, shouldCleanSensitiveData);
        }
        catch (JsonException)
        {
            return Enumerable.Empty<ExternalLoginProvider>();
        }
    }

    private static IEnumerable<IExternalLoginProvider> ParseProviders(JsonElement root, bool shouldCleanSensitiveData)
    {
        if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("AuthenticationProviders", out var providersElement))
        {
            return providersElement.ValueKind == JsonValueKind.Array
                ? (providersElement.Deserialize<IEnumerable<ExternalLoginProvider>>() ?? Array.Empty<ExternalLoginProvider>()).Select(provider =>
                {
                    if(shouldCleanSensitiveData) 
                        provider.CleanSensitiveData();
                    return provider;
                })
                : Enumerable.Empty<ExternalLoginProvider>();
        }

        if (root.ValueKind == JsonValueKind.Array)
        {
            return root.Deserialize<IEnumerable<ExternalLoginProvider>>() ?? Enumerable.Empty<ExternalLoginProvider>();
        }

        return Enumerable.Empty<ExternalLoginProvider>();
    }

    public async Task<IExternalLoginProvider> GetGoogleProviderConfiguration()
    {
        var configuredProviders = await GetAllProviders(false);
        return configuredProviders.Single(p => p.ProviderName.Equals("Google", StringComparison.OrdinalIgnoreCase));
    }
}