using ApexCharts;
using DNSLab.Web.Components;
using DNSLab.Web.Exceptions;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Middlewares;
using DNSLab.Web.Providers;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddScoped<HttpResponseExceptionHander>();
builder.Services.AddScoped<IHttpServiceProvider, HttpServiceProvider>();

//Repository
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IZoneRepository, ZoneRepository>();
builder.Services.AddScoped<IRecordRepository, RecordRepository>();
builder.Services.AddScoped<IDNSClientRepository, DNSClientRepository>();
builder.Services.AddScoped<IDDNSRepository, DDNSRepository>();
builder.Services.AddScoped<IStaticRepository, StaticRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IPageRepository, PageRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IDNSLogRepository, DNSLogRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<IToolRepository, ToolRepository>();
builder.Services.AddScoped<IReverseProxyRepository, ReverseProxyRepository>();


builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, BlazorAuthorizationMiddlewareResultHandler>();

builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<JWTAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());
builder.Services.AddScoped<IAuthenticationProvider>(provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());

builder.Services.AddMemoryCache();

//Apex Chart
builder.Services.AddApexCharts();

// Add MudBlazor services
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.BackgroundBlurred = true;
    config.SnackbarConfiguration.NewestOnTop = true;
    config.SnackbarConfiguration.ShowCloseIcon = false;
    config.SnackbarConfiguration.MaxDisplayedSnackbars = 3;
    config.SnackbarConfiguration.IconSize = MudBlazor.Size.Small;
    config.SnackbarConfiguration.VisibleStateDuration = 2500;
    config.SnackbarConfiguration.HideTransitionDuration = 300;
    config.SnackbarConfiguration.ShowTransitionDuration = 100;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddCircuitOptions(option =>
    {
        //only add details when debugging
        option.DetailedErrors = builder.Environment.IsDevelopment();
    });

builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.MaximumReceiveMessageSize = 3 * 1024 * 1024; // 3MB
});


builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("DNSLab-Web"))
    .WithMetrics(metrics =>
    {
        metrics.AddAspNetCoreInstrumentation(); 
        metrics.AddHttpClientInstrumentation(); 
        metrics.AddPrometheusExporter();        
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.Run();
