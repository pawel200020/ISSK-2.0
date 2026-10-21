using Data;
using Microsoft.EntityFrameworkCore;
using PortalBlazor.Components;
using PortalBlazor.Components.Account;
using Users.Core;
using Blazored.Modal;
using PortalBlazor.Extension;
using PortalBlazor.Middlewares.Extension;
using PortalBlazor.Toasts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents(options => 
        options.DetailedErrors = true)
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddLocalization(options => options.ResourcesPath = "../Resources/PortalResources");
builder.Services.AddSingleton<ToastMessageCreationService>();
builder.Services.AddBlazoredModal();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

builder.Services.AddPortalAuthentication();

builder.Services.AddServerSideBlazor().AddCircuitOptions(option =>
{
    option.DetailedErrors = true;
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddUsers();
builder.Services.AddBlazorBootstrap();
builder.Services.AddMemoryCache();

builder.Services.AddServices(connectionString);

var app = builder.Build();

await app.InitializePortalAuthenticationAsync();

app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseCustomMiddleware();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
