using Users.Interfaces.Managers;
using Users.Interfaces.Repositories;

namespace Users.Managers;

internal class UsersRemover : IUsersRemover
{
    private IUsersRepository _usersRepository;

    public UsersRemover(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
    }

    public async Task<bool> DeleteUser(Guid userId) 
        => await _usersRepository.RemoveUserAsync(userId);
}