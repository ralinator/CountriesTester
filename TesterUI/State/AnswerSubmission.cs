using System.ComponentModel.DataAnnotations;

namespace TesterUI.State;

public class AnswerSubmission
{
    [Required]
    public string? CountryCode { get; set; }
}
