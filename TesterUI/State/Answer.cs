namespace TesterUI.State;

public class Answer
{
    public Answer(string correctAnswerCountryCode, string correctAnswerContinentCode, string givenAnswerCountryCode, string givenAnswerContinentCode)
    {
        CorrectAnswerCountryCode = correctAnswerCountryCode;
        CorrectAnswerContinentCode = correctAnswerContinentCode;
        GivenAnswerCountryCode = givenAnswerCountryCode;
        GivenAnswerContinentCode = givenAnswerContinentCode;
    }

    public string CorrectAnswerCountryCode { get; set; }
    public string CorrectAnswerContinentCode { get; set; }
    public string GivenAnswerCountryCode { get; set; }
    public string GivenAnswerContinentCode { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public bool IsCorrectAnswer => CorrectAnswerCountryCode == GivenAnswerCountryCode;
    public bool IsCorrectContinent => CorrectAnswerContinentCode == GivenAnswerContinentCode;
}