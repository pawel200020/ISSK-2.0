namespace Users.Shared.Models;

public interface IUsersPaginatedList
{
    IEnumerable<IUser> Users { get; }
    int TotalCount { get; }
}