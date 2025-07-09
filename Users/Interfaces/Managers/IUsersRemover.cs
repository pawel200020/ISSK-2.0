namespace Users.Interfaces.Managers;

public interface IUsersRemover
{
    Task<bool> DeleteUser(Guid userId);
}