namespace ViewModels.RazorPages.EventSaver.Lines;

public class LineViewModel
{
    public string Id { get; set; }
    public string Number { get; set; }
    public DateOnly? Date { get; set; }
    public string Vehicle { get; set; }
    public string Supervisor { get; set; }
    public string SeasonId { get; set; }
    public LineType LineType { get; set; }
    public bool IsActive { get; set; }
}