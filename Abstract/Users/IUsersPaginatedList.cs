namespace Abstract.Users;

public interface IUsersPaginatedList
{
    IEnumerable<IUser> Users { get; }
    int TotalCount { get; }
}