using System.ComponentModel.DataAnnotations;
using Resources.PortalResources;

namespace ViewModels.RazorPages.Configuration;

public class EmailModel : IValidatableObject
{
    private const string EmailRegex = @"^(?=.{1,254}$)(?=.{1,64}@)[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:(?:[A-Za-z0-9](?:[A-Za-z0-9-]{0,61}[A-Za-z0-9])?\.)+[A-Za-z]{2,})$";

    public string Email { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Email) || !System.Text.RegularExpressions.Regex.IsMatch(Email, EmailRegex))
            yield return new ValidationResult(PortalResources.cCorrectEmailFormat, [nameof(Email)]);
    }
}

