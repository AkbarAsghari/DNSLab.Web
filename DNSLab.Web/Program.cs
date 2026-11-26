using ApexCharts;
using DNSLab.Web.API;
using DNSLab.Web.API.Abstractions;
using DNSLab.Web.API.Authentication;
using DNSLab.Web.API.Billing;
using DNSLab.Web.API.Coupon;
using DNSLab.Web.API.Page;
using DNSLab.Web.API.Profile;
using DNSLab.Web.API.Subscription;
using DNSLab.Web.API.Ticket;
using DNSLab.Web.API.Wallet;
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
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddScoped<HttpResponseExceptionHander>();
builder.Services.AddScoped<IHttpServiceProvider, HttpServiceProvider>();

//Repository
builder.Services.AddScoped<IZoneRepository, ZoneRepository>();
builder.Services.AddScoped<IRecordRepository, RecordRepository>();
builder.Services.AddScoped<IDNSClientRepository, DNSClientRepository>();
builder.Services.AddScoped<IDDNSRepository, DDNSRepository>();
builder.Services.AddScoped<IStaticRepository, StaticRepository>();
builder.Services.AddScoped<IDNSLogRepository, DNSLogRepository>();
builder.Services.AddScoped<IToolRepository, ToolRepository>();
builder.Services.AddScoped<IReverseProxyRepository, ReverseProxyRepository>();
builder.Services.AddScoped<IReverseProxyTrafficRepository, ReverseProxyTrafficRepository>();

////////////////API////////////////
builder.Services.Configure<ApiClientOptions>(
    builder.Configuration.GetSection(ApiClientOptions.SectionName));

builder.Services.AddHttpClient<IApiClient, ApiClient>((sp, client) =>
{
    var options = sp.GetRequiredService<
        IOptions<ApiClientOptions>>().Value;

    client.BaseAddress = new Uri(options.BaseAddress);
    client.Timeout = TimeSpan.FromSeconds(options.Timeout);
});

builder.Services.AddScoped<IAuthenticationApiClient, AuthenticationApiClient>();
builder.Services.AddScoped<IProfileApiClient, ProfileApiClient>();
builder.Services.AddScoped<IWalletApiClient, WalletApiClient>();
builder.Services.AddScoped<IPageApiClient, PageApiClient>();
builder.Services.AddScoped<ITicketApiClient, TicketApiClient>();
builder.Services.AddScoped<ICouponApiClient, CouponApiClient>();
builder.Services.AddScoped<ISubscriptionApiClient, SubscriptionApiClient>();
builder.Services.AddScoped<IPaymentApiClient, PaymentApiClient>();

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
