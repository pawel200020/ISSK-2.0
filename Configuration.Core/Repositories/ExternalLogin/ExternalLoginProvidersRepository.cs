using Configuration.Shared;
using Configuration.Shared.AppParameters.Managers;
using Data;

namespace Configuration.Repositories.ExternalLogin;

public class ExternalLoginProvidersRepository : IExternalLoginProvidersRepository
{
    private readonly IAppParameterGetter _appParameterGetter;
    public ExternalLoginProvidersRepository( IAppParameterGetter appParameterGetter)
    {
        _appParameterGetter = appParameterGetter ?? throw new ArgumentNullException(nameof(appParameterGetter));
    }
    public async Task<string> GetExternalLoginProviders()
    {
        return await _appParameterGetter.GetStringParameterValue(ApplicationParameter.ExternalLoginProviders) ?? string.Empty;
    }
}