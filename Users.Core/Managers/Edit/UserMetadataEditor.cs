using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Users.Core.Repositories.Edit;
using Users.Core.Repositories.Read;
using Users.Shared.Managers.Edit;
using Users.Shared.Models;

namespace Users.Core.Managers.Edit;

internal class UserMetadataEditor : IUserMetadataEditor
{
    private readonly IEditUsersRepository _editUsersRepository;

    public UserMetadataEditor(IEditUsersRepository editUsersRepository)
    {
        _editUsersRepository = editUsersRepository ?? throw new ArgumentNullException(nameof(editUsersRepository));
    }

    public async Task<bool> ChangeUserEmail(Guid userId, string email, string token) 
        => await _editUsersRepository.ChangeEmailAsync(userId, email, token);

    public async Task<string> Disable2FaAuthentication(Guid userId)
        => await _editUsersRepository.Disable2FaAuthentication(userId);

    
}