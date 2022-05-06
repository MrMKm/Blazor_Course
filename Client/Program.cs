using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestProject.Client.Services.Implementations;
using TestProject.Client.Services.Interfaces;

namespace TestProject.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection Services)
        {
            Services.AddSingleton<ServiceSingleton>();
            Services.AddTransient<ServiceTransient>();

            Services.AddScoped<IMovieAPI, MovieAPI>();
            Services.AddScoped<IGenderAPI, GenderAPI>();
            Services.AddScoped<IActorAPI, ActorAPI>();
            Services.AddScoped<IMovieAPI, MovieAPI>();
        }
    }
}
