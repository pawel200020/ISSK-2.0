using Abstract.Users;
using Users.Interfaces;
using Users.Interfaces.Managers;
using Users.Interfaces.Repositories;

namespace Users.Managers;

internal class UsersUploader : IUsersUploader
{
    private IUsersRepository _usersRepository;

    public UsersUploader(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
    }

   public async Task<IUserCreationResult> RegisterUser(IUser user)
   {
       return await _usersRepository.RegisterUser(user);
   }

   public async Task<bool> EditUserAsync(IUser user) 
       => await _usersRepository.EditUserAsync(user);


}