using ViewModels.RazorPages.EventSaver.Lines.Brigades;
using ViewModels.RazorPages.EventSaver.Seasons;
using ViewModels.RazorPages.Users;

namespace ViewModels.RazorPages.EventSaver.Lines;

public class LineViewModel
{
    public string Id { get; set; }
    public string Number { get; set; }
    public DateOnly? Date { get; set; }
    public string Vehicle { get; set; }
    public IList<UserMetadataViewModel> Supervisor { get; set; } = new List<UserMetadataViewModel>();
    public SeasonViewModel? Season { get; set; }
    public LineType LineType { get; set; }
    public bool IsActive { get; set; }
    public IList<BrigadeViewModel> Brigades { get; set; } = new List<BrigadeViewModel>();
}