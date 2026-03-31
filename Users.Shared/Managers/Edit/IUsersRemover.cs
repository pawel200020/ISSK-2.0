namespace Users.Shared.Managers.Edit;

public interface IUsersRemover
{
    Task<bool> DeleteUser(Guid userId);
    Task<string> DeleteUserWithPasswordCheck(Guid userId, string inputPassword);
}