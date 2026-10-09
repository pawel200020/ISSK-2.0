namespace Users.Shared.SignIn;

public enum SignInStatus
{
    Success,
    Failed,
    RequiresTwoFactor,
    LockedOut,
}