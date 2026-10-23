using System.ComponentModel.DataAnnotations;
using Resources.PortalResources;

namespace ViewModels.Validation;

public sealed class LocalizedRequiredAttribute : RequiredAttribute
{
    public LocalizedRequiredAttribute()
    {
        ErrorMessageResourceType = typeof(PortalResources);
        ErrorMessageResourceName = nameof(PortalResources.cRequiredField);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        LocalizedDisplayAttribute.ApplyTo(validationContext);
        return base.IsValid(value, validationContext);
    }
}
