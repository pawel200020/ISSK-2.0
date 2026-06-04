namespace ViewModels.RazorPages.EventSaver.Lines;

public class LineViewModel
{
    public Guid Id { get; set; }
    public required string Number { get; set; }
    public DateOnly Date { get; set; }
    public required string Vehicle { get; set; }
    public required string Supervisor { get; set; }
    public required string SeasonId { get; set; }
    public required LineType LineType { get; set; }
    public bool IsActive { get; set; }
}