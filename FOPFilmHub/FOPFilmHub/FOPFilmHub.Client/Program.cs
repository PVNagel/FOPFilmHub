using FOPFilmHub.Client;
using FOPFilmHub.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace FOPFilmHub.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5160") });
            builder.Services.AddMudServices();

            // Register UserClientService, FilmClientService
            builder.Services.AddScoped<IUserClientService, UserClientService>();
            builder.Services.AddScoped<IFilmClientService, FilmClientService>();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

            await builder.Build().RunAsync();
        }
    }
}