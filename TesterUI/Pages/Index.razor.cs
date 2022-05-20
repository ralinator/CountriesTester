using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;
using TesterUI.Services;
using TesterUI.State;

namespace TesterUI.Pages;

public partial class Index
{
    [Inject]
    private GameStateService GameStateService { get; set; } = null!;

    private NewGameForm _newGameForm = new();
    private AnswerSubmission _answerSubmission = new();

    protected override async Task OnInitializedAsync()
    {
        await GameStateService.InitializeAsync();
        await base.OnInitializedAsync();
    }

    private async Task OnNewGame()
    {
        if (_newGameForm.GameCode is not null)
        {
            await GameStateService.StartNewGameAsync(_newGameForm.GameCode);
        }
    }

    private async Task OnAnswerSubmission()
    {
        await GameStateService.SubmitAnswer(_answerSubmission);
        _answerSubmission = new();
    }
    
    private async Task ResumeGame(string id)
    {
        await GameStateService.ResumeGame(id);
    }
    private async Task DeleteGame(string id)
    {
        await GameStateService.DeleteGame(id);
    }
}