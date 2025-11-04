namespace PortalBlazor.Middlewares.Extension;

public static class CustomMiddlewareExtension
{
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app) 
        => app.UseMiddleware<CustomRequestLocalizationMiddleware>();
}