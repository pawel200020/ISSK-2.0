using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Resources.PortalResources;

namespace ViewModels.Validation;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class LocalizedDisplayAttribute : Attribute
{
    private readonly DisplayAttribute display;

    public LocalizedDisplayAttribute(string resourceName)
    {
        display = new DisplayAttribute
        {
            Name = resourceName,
            ResourceType = typeof(PortalResources)
        };
    }

    public string GetName() => display.GetName()!;

    internal static void ApplyTo(ValidationContext context)
    {
        if (context.MemberName is null)
            return;

        var attribute = context.ObjectType.GetProperty(context.MemberName)
            ?.GetCustomAttribute<LocalizedDisplayAttribute>();

        if (attribute is not null)
            context.DisplayName = attribute.GetName();
    }
}
