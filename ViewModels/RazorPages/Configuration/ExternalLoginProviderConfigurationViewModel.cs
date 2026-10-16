using System.ComponentModel.DataAnnotations;
using Resources.PortalResources;

namespace ViewModels.RazorPages.Configuration;

public class ExternalLoginProviderConfigurationViewModel : IValidatableObject
{
    public int ProviderType { get; set; }
    public string ProviderName { get; set; } = "";
    public string ClientId { get; set; } = "";
    public string ClientSecret { get; set; } = "";
    public bool IsEnabled { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (IsEnabled)
        {
            if (string.IsNullOrWhiteSpace(ClientId))
                yield return new ValidationResult(PortalResources.cRequiredField, [nameof(ClientId)]);
            if (string.IsNullOrWhiteSpace(ClientSecret))
                yield return new ValidationResult(PortalResources.cRequiredField, [nameof(ClientSecret)]);
        }
    }
}