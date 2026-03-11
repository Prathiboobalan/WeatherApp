using MudBlazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp.Services;
using BlazorApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddMudServices();
builder.Services.AddScoped<WeatherService>();
builder.Services.AddScoped<SupabaseClientService>();


await builder.Build().RunAsync();
