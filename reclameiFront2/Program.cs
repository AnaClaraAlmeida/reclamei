using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using reclameiFront2;
using reclameiFront2.Helpers;
using System.Net.Http;
using System;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Adicionando HttpClient com a URL base do Host
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Adicionando Api com a URL base da API
builder.Services.AddScoped(sp => new Api("https://localhost:7057"));

await builder.Build().RunAsync();
