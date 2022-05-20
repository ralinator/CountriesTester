using Microsoft.AspNetCore.Components;
using TesterUI.Services;
using TesterUI.State;

namespace TesterUI.Pages
{
    public partial class NewGameComponent
    {
        [Inject]
        private GameStateService GameStateService { get; set; } = null!;

        [Parameter]
        public EventCallback ValidSubmitCallback { get; set; }

        private NewGameForm _newGameForm = new();



        private async Task OnNewGame()
        {
            if (_newGameForm.GameCode is not null)
            {
                await GameStateService.StartNewGameAsync(_newGameForm.GameCode);
                _newGameForm = new();
                await ValidSubmitCallback.InvokeAsync();
            }
        }
    }
}