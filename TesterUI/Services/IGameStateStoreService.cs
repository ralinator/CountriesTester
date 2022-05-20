using Microsoft.JSInterop;
using System.Text.Json;
using TesterUI.State;

namespace TesterUI.Services;

public interface IGameStateStoreService
{
    Task<GameState?> GetStateAsync();
    Task SaveStateAsync(GameState gameState);
}

public class GameStateStoreService : IGameStateStoreService
{
    private readonly IJSRuntime _js;
    private readonly string _storageKey = "gameState";

    public GameStateStoreService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<GameState?> GetStateAsync()
    {
        var jsonString = await _js.InvokeAsync<string>("checkLocalStorage", new object?[] { _storageKey });
        if (jsonString is null)
        {
            return null;
        }
        return JsonSerializer.Deserialize<GameState?>(jsonString);
    }

    public async Task SaveStateAsync(GameState gameState)
    {
        await _js.InvokeVoidAsync("setLocalStorage", new object?[] { _storageKey, JsonSerializer.Serialize(gameState) });
    }
}
