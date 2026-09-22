using System.ComponentModel.DataAnnotations;
using Resources.PortalResources;
using ViewModels.RazorPages.EventSaver.Lines.Brigades;
using ViewModels.RazorPages.EventSaver.Seasons;
using ViewModels.RazorPages.Users;

namespace ViewModels.RazorPages.EventSaver.Lines;

public class LineViewModel
{
    public string Id { get; set; }
    [Required(ErrorMessageResourceName = "cRequiredField",  ErrorMessageResourceType = typeof(PortalResources))] 
    public string Number { get; set; }
    [Required(ErrorMessageResourceName = "cRequiredField",  ErrorMessageResourceType = typeof(PortalResources))] 
    public DateOnly? Date { get; set; }
    [Required(ErrorMessageResourceName = "cRequiredField",  ErrorMessageResourceType = typeof(PortalResources))] 
    public UserMetadataViewModel? Supervisor { get; set; }
    [Required(ErrorMessageResourceName = "cRequiredField",  ErrorMessageResourceType = typeof(PortalResources))] 
    public SeasonViewModel? Season { get; set; }
    public LineType LineType { get; set; }
    public bool IsActive { get; set; }
    public IList<BrigadeViewModel> Brigades { get; set; } = new List<BrigadeViewModel>();
}