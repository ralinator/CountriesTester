namespace TesterUI.State;

public class GameState
{
    public GameState(Game currentGame)
    {
        CurrentGame = currentGame;
    }

    public Game CurrentGame { get; set; }
    public List<Game> GameHistory { get; set; } = new();
}
