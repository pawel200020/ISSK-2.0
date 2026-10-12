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

    public async Task<IEnumerable<IExternalLoginProvider>> GetAllProviders()
    {
        var providersJson = await _repository.GetExternalLoginProviders();
        if (string.IsNullOrWhiteSpace(providersJson))
        {
            return Enumerable.Empty<ExternalLoginProvider>();
        }

        try
        {
            using var document = JsonDocument.Parse(providersJson);
            return ParseProviders(document.RootElement);
        }
        catch (JsonException)
        {
            return Enumerable.Empty<ExternalLoginProvider>();
        }
    }

    private static IEnumerable<IExternalLoginProvider> ParseProviders(JsonElement root)
    {
        if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("AuthenticationProviders", out var providersElement))
        {
            return providersElement.ValueKind == JsonValueKind.Array
                ? providersElement.Deserialize<IEnumerable<ExternalLoginProvider>>() ?? Enumerable.Empty<ExternalLoginProvider>()
                : Enumerable.Empty<ExternalLoginProvider>();
        }

        if (root.ValueKind == JsonValueKind.Array)
        {
            return root.Deserialize<IEnumerable<ExternalLoginProvider>>() ?? Enumerable.Empty<ExternalLoginProvider>();
        }

        return Enumerable.Empty<ExternalLoginProvider>();
    }
}