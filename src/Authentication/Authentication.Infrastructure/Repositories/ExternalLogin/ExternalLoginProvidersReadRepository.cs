using Authentication.Application.Repositiories.ExternalLogin;
using Authentication.Domain.ExternalLogin;
using Authentication.Infrastructure.Caching;
using Authentication.Shared.ExternalLogin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Authentication.Infrastructure.Repositories.ExternalLogin;

internal class ExternalLoginProvidersReadRepository : IExternalLoginProvidersReadRepository
{
    private readonly AuthenticationDbContext _context;
    private readonly IMemoryCache _memoryCache;

    public ExternalLoginProvidersReadRepository(AuthenticationDbContext context, IMemoryCache memoryCache)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    public async Task<IEnumerable<ExternalLoginProvider>> GetExternalLoginProvidersMetadata(bool cleanSensitiveData)
    {
        if(_memoryCache.TryGetValue(CacheKeys.ExternalLoginProvidersMetadata, out IEnumerable<ExternalLoginProvider>? cachedMetadata))
        {
            if (cachedMetadata != null) return cachedMetadata;
        }

        var externalLoginProviders = await _context.ExternalLoginProviders.AsNoTracking().ToListAsync();
        var metadata = externalLoginProviders.Select(p => new ExternalLoginProvider
        {
            Guid = p.Id,
            Name = p.Name,
            ProviderType = (ProviderType)p.ProviderType,
            IsEnabled = p.IsEnabled,
            ClientId = p.ClientId,
            ClientSecret = cleanSensitiveData ? null : p.ClientSecret,
            IsClientSecretConfigured = !string.IsNullOrWhiteSpace(p.ClientSecret),
        });
        _memoryCache.CreateEntry(CacheKeys.ExternalLoginProvidersMetadata)
            .SetValue(metadata)
            .SetSlidingExpiration(TimeSpan.FromDays(30));
        
        return metadata;
    }

    public async Task<ExternalLoginProviderMetadata?> GetGoogleExternalLoginProvidersMetadata()
    {
        if(_memoryCache.TryGetValue(CacheKeys.ExternalLoginProvidersMetadata, out IEnumerable<ExternalLoginProvider>? cachedMetadata))
        {
            var googleProvider = cachedMetadata?.SingleOrDefault(p => p.ProviderType == ProviderType.Google);
            return new ExternalLoginProviderMetadata()
            {
                ClientId = googleProvider?.ClientId,
                ClientSecret = googleProvider?.ClientSecret,
                IsEnabled = googleProvider?.IsEnabled ?? false
            };
        }
        
        var externalLoginProvider = await _context.ExternalLoginProviders.AsNoTracking()
            .SingleOrDefaultAsync(p => p.ProviderType == (Entities.ExternalLoginProviders.ProviderType)ProviderType.Google);

        if (externalLoginProvider is null)
            return null;
        
        var metadata = new ExternalLoginProviderMetadata()
        {
            ClientId = externalLoginProvider.ClientId,
            ClientSecret = externalLoginProvider.ClientSecret,
            IsEnabled = externalLoginProvider.IsEnabled
        };
        
        return metadata;
    }
}
