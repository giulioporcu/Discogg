using Application.Services;
using Discogs.API.Framework;
using Discogs.API.Framework.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

namespace Application
{
    /// <summary>
    /// The entry point of the Blazor WebAssembly application.
    /// Responsible for configuring root components, registering services,
    /// initializing MudBlazor, and starting the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Configures and builds the WebAssembly host, registers application services,
        /// sets up UI components, and starts the Blazor application.
        /// </summary>
        /// <param name="args">Command-line arguments passed to the application.</param>
        public static async Task Main(string[] args)
        {
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Clients
            builder.Services.AddScoped(httpClientProvider =>
                new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Services
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<DiscogsSettings>();
            builder.Services.AddScoped<ThemeService>();
            builder.Services.AddScoped<VersionInfoService>();
            builder.Services.AddScoped<LocalStorageService>();
            builder.Services.AddScoped<JsonSerializationService>();
            builder.Services.AddScoped<AuthenticationService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<DiscogsClient>();

            // MudBlazor
            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopLeft;
                config.SnackbarConfiguration.HideTransitionDuration = 150;
                config.SnackbarConfiguration.ShowTransitionDuration = 150;
                config.SnackbarConfiguration.VisibleStateDuration = 2000;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
                config.SnackbarConfiguration.BackgroundBlurred = false;
                config.SnackbarConfiguration.RequireInteraction = false;
                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
            });

            await builder.Build().RunAsync();
        }
    }
}