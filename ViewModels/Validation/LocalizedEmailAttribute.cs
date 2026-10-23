using System.ComponentModel.DataAnnotations;
using Resources.PortalResources;

namespace ViewModels.Validation;

public sealed class LocalizedEmailAttribute : RegularExpressionAttribute
{
    private const string EmailRegex = @"^(?=.{1,254}$)(?=.{1,64}@)[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:(?:[A-Za-z0-9](?:[A-Za-z0-9-]{0,61}[A-Za-z0-9])?\.)+[A-Za-z]{2,})$";

    public LocalizedEmailAttribute() : base(EmailRegex)
    {
        ErrorMessageResourceType = typeof(PortalResources);
        ErrorMessageResourceName = nameof(PortalResources.cCorrectEmailFormat);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        LocalizedDisplayAttribute.ApplyTo(validationContext);
        return base.IsValid(value, validationContext);
    }
}
