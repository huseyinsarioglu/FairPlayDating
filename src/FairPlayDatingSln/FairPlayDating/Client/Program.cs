using FairPlayDating.Client.ClientServices;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string assemblyName = "FairPlayDating";
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient($"{assemblyName}.ServerAPI", client =>
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddHttpClient($"{assemblyName}.ServerAPI.Anonymous", client =>
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient($"{assemblyName}.ServerAPI"));

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient($"{assemblyName}.ServerAPI.Anonymous"));

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
                var defaultScope = builder.Configuration["AzureAdB2CScopes:DefaultScope"];
                options.ProviderOptions.DefaultAccessTokenScopes.Add(defaultScope);
                options.ProviderOptions.LoginMode = "redirect";
                options.UserOptions.NameClaim = "name";
                options.UserOptions.RoleClaim = "Role";
            });

            builder.Services.AddScoped<HttpClientService>();
            builder.Services.AddScoped<FacebookClientService>();
            await builder.Build().RunAsync();
        }
    }
}
