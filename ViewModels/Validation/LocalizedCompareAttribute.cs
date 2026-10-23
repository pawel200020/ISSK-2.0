using System.ComponentModel.DataAnnotations;
using Resources.PortalResources;

namespace ViewModels.Validation;

public sealed class LocalizedCompareAttribute : CompareAttribute
{
    public LocalizedCompareAttribute(string otherProperty) : base(otherProperty)
    {
        ErrorMessageResourceType = typeof(PortalResources);
        ErrorMessageResourceName = nameof(PortalResources.cPasswordMismatchError);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        LocalizedDisplayAttribute.ApplyTo(validationContext);
        return base.IsValid(value, validationContext);
    }
}
