using System.ComponentModel.DataAnnotations;

namespace ViewModels.RazorPages.EventSaver.Seasons;

public class SeasonViewModel
{
    public string SeasonId { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    public required string Name { get; set; }

    [Required]
    public DateOnly? StartDate { get; set; }

    [Required]
    public DateOnly? EndDate { get; set; }
}