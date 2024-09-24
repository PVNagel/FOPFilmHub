using FOPFilmHub.Client;
using FOPFilmHub.Client.Services.Movie;
using FOPFilmHub.Client.Services.User;
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

            builder.Services.AddAuthorizationCore();
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

            // Register UserClientService
            builder.Services.AddScoped<IUserClientService, UserClientService>();

            await builder.Build().RunAsync();
        }
    }
}
