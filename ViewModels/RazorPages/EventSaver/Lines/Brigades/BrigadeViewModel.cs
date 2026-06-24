using ViewModels.RazorPages.Users;

namespace ViewModels.RazorPages.EventSaver.Lines.Brigades;

public class BrigadeViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public TimeOnly StartHour { get; set; }
    public TimeOnly EndHour { get; set; }
    public double Points {get; set;}
    public IEnumerable<UserMetadataViewModel> People { get; set; } = new List<UserMetadataViewModel>();
}