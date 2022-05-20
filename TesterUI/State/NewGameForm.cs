using System.ComponentModel.DataAnnotations;

namespace TesterUI.State;

public class NewGameForm
{
    [Required]
    public string? GameCode { get; set; }
}
