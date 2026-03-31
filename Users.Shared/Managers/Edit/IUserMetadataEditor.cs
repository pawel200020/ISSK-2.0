namespace Users.Shared.Managers.Edit;

public interface IUserMetadataEditor
{
    Task<bool> ChangeUserEmail(Guid userId, string email, string token);

    Task<string> Disable2FaAuthentication(Guid userId);
}