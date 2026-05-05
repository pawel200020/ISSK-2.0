namespace Users.Shared.Password;

public interface IUserResetPasswordEmailSender
{
    Task SendResetPasswordEmail(string email);
}