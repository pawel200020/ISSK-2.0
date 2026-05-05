using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Notifications.Shared.Email;
using Resources.PortalResources;
using Users.Core.Repositories.Edit;
using Users.Core.Repositories.Read;
using Users.Shared.Password;

namespace Users.Core.Password;

internal class UserResetPasswordEmailSender : IUserResetPasswordEmailSender
{
    private readonly IEditUsersRepository _editUsersRepository;
    private readonly IReadUsersRepository _userReadRepository;
    private readonly NavigationManager _navigationManager;
    private readonly IEmailService _emailService;

    public UserResetPasswordEmailSender(IEditUsersRepository editUsersRepository, NavigationManager navigationManager, IEmailService emailService, IReadUsersRepository userReadRepository)
    {
        _editUsersRepository = editUsersRepository ?? throw new ArgumentNullException(nameof(editUsersRepository));
        _navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _userReadRepository = userReadRepository ?? throw new ArgumentNullException(nameof(userReadRepository));
    }

    public async Task SendResetPasswordEmail(string email)
    {
        var user = await _userReadRepository.TryGetUserByEmail(email);
        if (user == null)
            return;
        
        var userId = new Guid(user.Id);
        var token = await _editUsersRepository.GeneratePasswordResetTokenAsync(userId);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        var callbackUrl = _navigationManager.GetUriWithQueryParameters(
            _navigationManager.ToAbsoluteUri("Account/ResetPassword").AbsoluteUri,
            new Dictionary<string, object?> { ["code"] = encodedToken, ["email"] = email });

        await _emailService.SendEmail(email, PortalResources.cResetPasswordEmailSubject, string.Format(PortalResources.cResetPasswordEmailBody, callbackUrl));
    }
}