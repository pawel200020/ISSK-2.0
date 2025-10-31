using Abstract.Languages;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace PortalBlazor.Middlewares;

public class CustomRequestLocalizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerFactory _loggerFactory;

    public CustomRequestLocalizationMiddleware(RequestDelegate next,
        ILoggerFactory loggerFactory)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }

    public async Task Invoke(HttpContext context, ISupportedLanguagesDownloader supportedLanguagesDownloader /* You can inject services here, such as DbContext or IDbConnection*/)
    {
        // You can search your database for your supported and/or default languages here
        // This query will execute for all requests, so consider using caching
        var supportedLanguages = await supportedLanguagesDownloader.GetSupportedLanguages();
        var cultures = supportedLanguages.Select(l => l.Name).ToArray();
        var defaultCulture = cultures.First();

        // You can configure the options here as you would do by calling services.Configure<RequestLocalizationOptions>()
        var options = new RequestLocalizationOptions()
            .AddSupportedCultures(cultures)
            .AddSupportedUICultures(cultures)
            .SetDefaultCulture(defaultCulture);

        // Finally, we instantiate ASP.Net's default RequestLocalizationMiddleware and call it
        var defaultImplementation = new RequestLocalizationMiddleware(_next, Options.Create(options), _loggerFactory);
        await defaultImplementation.Invoke(context);
    }
}