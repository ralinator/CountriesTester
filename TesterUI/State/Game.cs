namespace TesterUI.State;

public class Game
{
    public Game(string gameCode, DateTime startTime)
    {
        GameCode = gameCode;
        StartTime = startTime;
    }

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime StartTime { get; private set; }
    public string GameCode { get; set; }
    public List<Answer> Answers { get; set; } = new();
    public List<string> UsedCountryCodes => Answers.Select(q => q.CorrectAnswerCountryCode).ToList();
    public int Score => Answers.Where(q => q.IsCorrectAnswer).Count();
    public int ContinentScore => Answers.Where(q => q.IsCorrectContinent).Count();
    public string? CurrentQuestionCountryCode { get; set; }
    public bool IsCurrentQuestionAnswered => Answers.Any(q => q.CorrectAnswerCountryCode == CurrentQuestionCountryCode);
    public bool IsCurrentQuestionAnsweredAndCorrect => Answers.Any(q => q.CorrectAnswerCountryCode == CurrentQuestionCountryCode && q.IsCorrectAnswer);
    public DateTime? CompletedTime { get; set; }
}
