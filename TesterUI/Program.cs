using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TesterUI;
using TesterUI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddSingleton<GameStateService>();
builder.Services.AddSingleton<IGameDataService, GameDataService>();
builder.Services.AddSingleton<IGameStateStoreService, GameStateStoreService>();

await builder.Build().RunAsync();
