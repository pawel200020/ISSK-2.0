namespace Authentication.Shared.ExternalLogin.Entities;

public interface IUserExternalLoginMetadata : IExternalLoginMetadata
{
    string UserName { get; }
    string? AccessToken { get; }
}