using System.ComponentModel.DataAnnotations;
using Resources.PortalResources;

namespace ViewModels.RazorPages.Configuration;

public class ApplicationConfigurationViewModel :IValidatableObject
{
    //[Required(ErrorMessageResourceType = typeof(PortalResources), ErrorMessageResourceName = "cRequiredField")]
    public required string ApplicationName { get; set; }
    public bool IsWeatherEnabled { get; set; }
    public bool IsRankingEnabled { get; set; }
    public bool IsSelfRegisterEnabled { get; set; }
    public int EmailSendMode { get; set; }
    public string? EmailLogin { get; set; }
    public string? EmailPassword { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(string.IsNullOrWhiteSpace(ApplicationName))
            yield return new ValidationResult(PortalResources.cRequiredField, [nameof(ApplicationName)]);
        
        if (EmailSendMode is (int)ViewModels.RazorPages.Configuration.EmailSendMode.MailTrap 
            or (int)ViewModels.RazorPages.Configuration.EmailSendMode.Smtp)
        {
            if(string.IsNullOrWhiteSpace(EmailLogin))
                yield return new ValidationResult(PortalResources.cRequiredField, [nameof(EmailLogin)]);
        }
    }
}