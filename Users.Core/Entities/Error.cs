namespace Users.Core.Entities;

public record Error
{
    public required ErrorReason Code { get; set; }
    public string? Description { get; set; }
}