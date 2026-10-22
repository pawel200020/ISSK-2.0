namespace Users.Shared.Authentication;

public enum AuthenticationResult
{
    Success,
    Failed,
    LockedOut,
    NotAllowed,
    TwoFactorRequired
}