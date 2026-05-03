using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Notifications.Shared.Email;
using Resources.PortalResources;
using Users.Core.Repositories.Edit;
using Users.Core.Repositories.Read;
using Users.Shared.Managers;
using Users.Shared.Models;

namespace Users.Core.Email;

internal class UserEmailConfirmation : IUserEmailConfirmation
{
    private readonly IEditUsersRepository _editUsersRepository;
    private readonly NavigationManager _navigationManager;
    private readonly IReadUsersRepository _readUsersRepository;
    private readonly IEmailService _emailService;

    public UserEmailConfirmation(IEditUsersRepository editUsersRepository, NavigationManager navigationManager,
        IReadUsersRepository readUsersRepository, IEmailSender<ApplicationUser> emailSender, IEmailService emailService)
    {
        _editUsersRepository = editUsersRepository ?? throw new ArgumentNullException(nameof(editUsersRepository));
        _navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
        _readUsersRepository = readUsersRepository ?? throw new ArgumentNullException(nameof(readUsersRepository));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

    public async Task<bool> ConfirmEmailAsync(Guid userId, string token)
    {
        var user = await _readUsersRepository.GetUserById(userId);
        return (await _editUsersRepository.ConfirmEmailAsync(user, token));
    }

    public async Task<bool> SendConfirmationNewEmailAndGenerateToken(Guid userId, string email)
    {
        var token = await _editUsersRepository.GenerateChangeEmailTokenAsync(userId, email);
        var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        return await SendConfirmationEmail(email, "ConfirmEmailChange",
            new Dictionary<string, object?> { ["userId"] = userId, ["email"] = email, ["code"] = code } );
    }
    
    public async Task<bool> SendConfirmationEmailAccountWithReturnUrlAndGenerateToken(Guid userId, string email, string returnUrl)
    {
        var token = await _editUsersRepository.GenerateEmailConfirmationTokenAsync(userId);
        var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        return await SendConfirmationEmail(email, "ConfirmEmail",
            new Dictionary<string, object?> { ["userId"] = userId, ["code"] = code, ["returnUrl"] = returnUrl });

    }

    public async Task<bool> SendConfirmEmailWithReturnUrl(Guid userId, string email, string token, string returnUrl)
    {
        var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        return await SendConfirmationEmail(email, "ConfirmEmail",
            new Dictionary<string, object?> { ["userId"] = userId, ["code"] = code, ["returnUrl"] = returnUrl });
    }


    public async Task<bool> SendConfirmationCurrentEmailAndGenerateToken(Guid userId)
    {
        var user = await _readUsersRepository.GetUserById(userId);
        var token = await _editUsersRepository.GenerateChangeEmailTokenAsync(userId, user.Email);
        var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        return await SendConfirmationEmail(user.Email, "ConfirmEmail",
            new Dictionary<string, object?> { ["userId"] = userId, ["code"] = code });
    }

    private async Task<bool> SendConfirmationEmail( string email, string confirmationUri,
        IReadOnlyDictionary<string, object?> parameters)
    {
        var callbackUrl = _navigationManager.GetUriWithQueryParameters(
            _navigationManager.ToAbsoluteUri($"Account/{confirmationUri}").AbsoluteUri, parameters);

        await _emailService.SendEmail(email,PortalResources.cConfirmationEmailTitle, string.Format(PortalResources.cConfirmationEmailBody, HtmlEncoder.Default.Encode(callbackUrl)));
        return true;
    }
}