using Abstract.Users;
#nullable disable
namespace Users.Models;

public class UsersPaginatedList : IUsersPaginatedList
{
    public IEnumerable<IUser> Users { get; init; }
    public int TotalCount { get; init; }
}