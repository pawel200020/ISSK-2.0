using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Users.Core.Repositories.Edit;
using Users.Core.Repositories.Read;
using Users.Shared.Managers;
using Users.Shared.Models;

namespace Users.Core.Managers;

internal class UserEmailConfirmation : IUserEmailConfirmation
{
    private readonly IEditUsersRepository _editUsersRepository;
    private readonly NavigationManager _navigationManager;
    private readonly IReadUsersRepository _readUsersRepository;
    private readonly IEmailSender<ApplicationUser> _emailSender;

    public UserEmailConfirmation(IEditUsersRepository editUsersRepository, NavigationManager navigationManager,
        IReadUsersRepository readUsersRepository, IEmailSender<ApplicationUser> emailSender)
    {
        _editUsersRepository = editUsersRepository ?? throw new ArgumentNullException(nameof(editUsersRepository));
        _navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
        _readUsersRepository = readUsersRepository ?? throw new ArgumentNullException(nameof(readUsersRepository));
        _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
    }

    public async Task<bool> ConfirmEmailAsync(ApplicationUser user, string token)
        => (await _editUsersRepository.ConfirmEmailAsync(user, token));

    public async Task<bool> SendConfirmationNewEmail(Guid userId, string email)
    {
        var token = await _editUsersRepository.GenerateChangeEmailTokenAsync(userId, email);
        var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        return await SendConfirmationEmail(userId, email, "ConfirmEmailChange",
            new Dictionary<string, object?> { ["userId"] = userId, ["email"] = email, ["code"] = code } );
    }
    
    public async Task<bool> SendConfirmationCurrentEmail(Guid userId)
    {
        var user = await _readUsersRepository.GetUserById(userId);
        var token = await _editUsersRepository.GenerateChangeEmailTokenAsync(userId, user.Email);
        var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        return await SendConfirmationEmail(userId, user.Email, "ConfirmEmail",
            new Dictionary<string, object?> { ["userId"] = userId, ["code"] = code });
    }

    private async Task<bool> SendConfirmationEmail(Guid userId, string email, string confirmationUri,
        IReadOnlyDictionary<string, object?> parameters)
    {
        var callbackUrl = _navigationManager.GetUriWithQueryParameters(
            _navigationManager.ToAbsoluteUri($"Account/{confirmationUri}").AbsoluteUri, parameters);

        await _emailSender.SendConfirmationLinkAsync(await _readUsersRepository.GetUserById(userId), email,
            HtmlEncoder.Default.Encode(callbackUrl));
        return true;
    }
}