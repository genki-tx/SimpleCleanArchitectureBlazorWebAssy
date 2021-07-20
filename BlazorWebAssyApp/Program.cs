using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using SCA;

namespace BlazorWebAssyApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Dependency Injection
            // You can also register as Scoped instance by AddScoped depending on how you want to use the components
            builder.Services.AddSingleton<ICountDBGateway, CountDBGateway>();
            builder.Services.AddSingleton<ICountUsecase, CountUsecase>();
            builder.Services.AddSingleton<ICountPresenter, CountPresenter>();

            await builder.Build().RunAsync();
        }
    }
}
