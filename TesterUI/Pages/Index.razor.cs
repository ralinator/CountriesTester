using Common;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System.Text.Json;
using TesterUI.Services;
using TesterUI.State;

namespace TesterUI.Pages;

public partial class Index
{
    [Inject]
    private GameStateService GameStateService { get; set; } = null!;

    private AnswerSubmission _answerSubmission = new();

    private IEnumerable<Country>? _countriesList;

    protected override async Task OnInitializedAsync()
    {
        await GameStateService.InitializeAsync();
        await base.OnInitializedAsync();
    }

    private void FilterCountriesList(LoadDataArgs args)
    {
        _countriesList = GameStateService.GameData.Countries.AsEnumerable();
        if (!string.IsNullOrEmpty(args.Filter))
        {
            _countriesList = _countriesList.Where(q =>
            q.Name.ToLower().Contains(args.Filter.ToLower()) ||
            (q.AlternativeName == null ? false : q.AlternativeName.ToLower().Contains(args.Filter.ToLower()))
            );
        }
        _countriesList = _countriesList.OrderBy(q => q.Name).ToList();
    }

    private async Task OnAnswerSubmission()
    {
        await GameStateService.SubmitAnswer(_answerSubmission);
        _answerSubmission = new();
        FilterCountriesList(new());
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