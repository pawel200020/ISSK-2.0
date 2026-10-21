using Authentication.Application.Repositiories.ExternalLogin;
using Authentication.Infrastructure.Caching;
using Authentication.Shared.ExternalLogin;
using Microsoft.Extensions.Caching.Memory;

namespace Authentication.Infrastructure.Repositories.ExternalLogin;

internal class ExternalLoginProvidersWriteRepository : IExternalLoginProvidersWriteRepository
{
    private readonly AuthenticationDbContext _context;
    private readonly IMemoryCache _memoryCache;

    public ExternalLoginProvidersWriteRepository(AuthenticationDbContext context, IMemoryCache memoryCache)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    public async Task UpdateExternalLoginProviderAsync(IExternalLoginProvider provider)
    {
        var currentProviderData = await _context.ExternalLoginProviders.FindAsync(provider.Guid);
        if (currentProviderData == null||
        (provider.ClientId == currentProviderData.ClientId &&
           (string.IsNullOrEmpty(provider.ClientSecret) || provider.ClientSecret == currentProviderData.ClientSecret) &&
           provider.IsEnabled == currentProviderData.IsEnabled))
            return;
        
        currentProviderData.ClientId = provider.ClientId;
        currentProviderData.IsEnabled = provider.IsEnabled;
        
        if (currentProviderData.ClientId != provider.ClientId ||
            (currentProviderData.ClientId == provider.ClientId && !string.IsNullOrEmpty(provider.ClientSecret)))
            currentProviderData.ClientSecret = provider.ClientSecret;
        
        await _context.SaveChangesAsync();
        _memoryCache.Remove(CacheKeys.ExternalLoginProvidersMetadata);
    }
}