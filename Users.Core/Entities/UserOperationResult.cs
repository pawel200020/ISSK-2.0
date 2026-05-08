namespace Users.Core.Entities;

internal record UserOperationResult
{
    public bool IsSuccess { get; set; }
    public IEnumerable<Error>? Messages { get; set; }
}