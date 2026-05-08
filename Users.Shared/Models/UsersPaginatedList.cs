namespace Users.Shared.Models;

public class UsersPaginatedList : IUsersPaginatedList
{
    public IEnumerable<IUser> Users { get; init; }
    public int TotalCount { get; init; }
}