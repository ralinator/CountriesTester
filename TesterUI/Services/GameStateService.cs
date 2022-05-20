using Common;
using TesterUI.State;

namespace TesterUI.Services;

public class GameStateService
{
    private readonly IGameStateStoreService _gameStateStoreService;
    private readonly IGameDataService _gameDataService;
    private GameState? _state;
    private GameData _gameData = null!;

    public GameStateService(IGameStateStoreService gameStateStoreService, IGameDataService gameDataService)
    {
        _gameStateStoreService = gameStateStoreService;
        _gameDataService = gameDataService;
    }

    public GameState? State => _state;
    public GameData GameData => _gameData;

    public string CurrentQuestionCountryName => GameData.Countries.Single(q => q.Code == State?.CurrentGame.CurrentQuestionCountryCode).Name;
    public string CurrentQuestionContinentName => GameData.Continents.Single(continent =>
    continent.Code == GameData.Countries.Single(q => q.Code == State?.CurrentGame.CurrentQuestionCountryCode).Continent
    ).Name;

    public string LastAnswerCountryName => GameData.Countries.Single(q =>
    q.Code == State?.CurrentGame.Answers.Single(a =>
    a.CorrectAnswerCountryCode == State?.CurrentGame.CurrentQuestionCountryCode).GivenAnswerCountryCode
    ).Name;
    public string LastAnswerContinentName => GameData.Continents.Single(continent =>
    continent.Code == GameData.Countries.Single(q =>
    q.Code == State?.CurrentGame.Answers.Single(a =>
    a.CorrectAnswerCountryCode == State?.CurrentGame.CurrentQuestionCountryCode).GivenAnswerCountryCode
    ).Continent
    ).Name;


    public async Task InitializeAsync()
    {
        _gameData = await _gameDataService.GetAsync();
        _state = await _gameStateStoreService.GetStateAsync();
    }

    public async Task StartNewGameAsync(string gameCode)
    {
        var newGame = new Game(gameCode);
        if (_state is null)
        {
            _state = new(newGame);
        }
        else
        {
            _state.GameHistory.Add(_state.CurrentGame);
            _state.CurrentGame = newGame;
        }
        await SetNewQuestion();
        await _gameStateStoreService.SaveStateAsync(_state);
    }

    public async Task SetNewQuestion()
    {
        if (_state is not null)
        {
            if (_state.CurrentGame.CompletedTime is null)
            {
                var random = new Random();
                IEnumerable<Country> countriesEnumerable = GetCountryEnumerable();
                int index;
                do
                {
                    index = random.Next(0, countriesEnumerable.Count());
                }
                while (_state.CurrentGame.Answers.Any(q => q.CorrectAnswerCountryCode == countriesEnumerable.ElementAt(index).Code));
                _state.CurrentGame.CurrentQuestionCountryCode = countriesEnumerable.ElementAt(index).Code;
                await _gameStateStoreService.SaveStateAsync(_state);
            }
        }
    }

    private IEnumerable<Country> GetCountryEnumerable()
    {
        var countriesEnumerable = _gameData.Countries.AsEnumerable();
        if (_gameData.Continents.Any(q => q.Code == _state!.CurrentGame.GameCode))
        {
            countriesEnumerable = countriesEnumerable.Where(q => q.Continent == _state!.CurrentGame.GameCode);
        }

        return countriesEnumerable;
    }

    public async Task SubmitAnswer(AnswerSubmission submission)
    {
        if (!string.IsNullOrEmpty(submission.CountryCode) && _state is not null)
        {
            if (_state.CurrentGame.IsCurrentQuestionAnswered == false)
            {
                var continentCode = _gameData.Countries.First(q => q.Code == submission.CountryCode).Continent;

                var correctContinentCode = _gameData.Countries.First(q => q.Code == _state.CurrentGame.CurrentQuestionCountryCode).Continent;

                var answer = new Answer(_state.CurrentGame.CurrentQuestionCountryCode!,
                    correctContinentCode,
                    submission.CountryCode,
                    continentCode);
                _state.CurrentGame.Answers.Add(answer);
                if (_state.CurrentGame.Answers.Count == GetCountryEnumerable().Count())
                {
                    _state.CurrentGame.CompletedTime = DateTime.Now;
                }
                await _gameStateStoreService.SaveStateAsync(_state);
            }
        }
    }

    public async Task ResumeGame(string id)
    {
        if (_state is not null)
        {
            _state.GameHistory.Add(_state.CurrentGame);
            _state.CurrentGame = _state.GameHistory.First(q => q.Id == id);
            _state.GameHistory.RemoveAll(q => q.Id == id);
            await _gameStateStoreService.SaveStateAsync(_state);
        }
    }

    public async Task DeleteGame(string id)
    {
        if (_state is not null)
        {
            _state.GameHistory.RemoveAll(q => q.Id == id);
            await _gameStateStoreService.SaveStateAsync(_state);
        }
    }
}
