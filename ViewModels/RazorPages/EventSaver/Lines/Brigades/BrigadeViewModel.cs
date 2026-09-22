using System.ComponentModel.DataAnnotations;
using Resources.PortalResources;
using ViewModels.RazorPages.Users;

namespace ViewModels.RazorPages.EventSaver.Lines.Brigades;

public class BrigadeViewModel
{
    public string Id { get; set; }
    [Required(ErrorMessageResourceName = "cRequiredField",  ErrorMessageResourceType = typeof(PortalResources))] 
    public string Name { get; set; }
    public TimeOnly StartHour { get; set; }
    public TimeOnly EndHour { get; set; }
    public int Capacity { get; set; }
    [Required(ErrorMessageResourceName = "cRequiredField",  ErrorMessageResourceType = typeof(PortalResources))] 
    public string Vehicle {get; set;}
    [Required(ErrorMessageResourceName = "cRequiredField",  ErrorMessageResourceType = typeof(PortalResources))] 
    public double Points {get; set;}
    public bool IsActive {get; set;}
    public UserMetadataViewModel? Coordinator { get; set; }
    public IList<UserMetadataViewModel> People { get; set; } = new List<UserMetadataViewModel>();
}