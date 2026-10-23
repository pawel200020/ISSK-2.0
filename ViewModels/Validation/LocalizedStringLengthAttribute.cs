using System.ComponentModel.DataAnnotations;
using Resources.PortalResources;

namespace ViewModels.Validation;

public sealed class LocalizedStringLengthAttribute : StringLengthAttribute
{
    public LocalizedStringLengthAttribute(int min, int max) : base(max)
    {
        MinimumLength = min;
        ErrorMessageResourceType = typeof(PortalResources);
        ErrorMessageResourceName = nameof(PortalResources.cPasswordLengthError);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        LocalizedDisplayAttribute.ApplyTo(validationContext);
        return base.IsValid(value, validationContext);
    }
}
