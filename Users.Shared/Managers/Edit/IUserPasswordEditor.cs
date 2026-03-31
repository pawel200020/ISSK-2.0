namespace Users.Shared.Managers.Edit;

public interface IUserPasswordEditor
{
    Task<IEnumerable<string>> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);
}