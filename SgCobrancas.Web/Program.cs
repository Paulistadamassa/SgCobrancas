using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SgCobrancas.Web;
using SgCobrancas.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Aponta para a porta fixa da sua Web API (ApiService)
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7123/")
});

builder.Services.AddScoped<IApiService, ApiService>();

await builder.Build().RunAsync();